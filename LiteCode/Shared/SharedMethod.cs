using LiteCode.Attributes;
using LiteCode.Messages;
using SecureSocketProtocol3.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LiteCode.Shared
{
    [Serializable]
    public class SharedMethod : IDisposable
    {
        public string Name { get; internal set; }
        public Type[] ArgumentTypes { get; internal set; }
        public bool CanReturn { get; internal set; }
        public Type ReturnType { get; internal set; }
        public SharedClass SharedClass { get; internal set; }
        internal int MethodId { get; set; }
        internal object InvokeLocky = new object();
        public bool Unchecked { get; internal set; }
        public SortedList<int, SharedDelegateInfo> DelegateIndex { get; private set; }
        public SortedList<int, SharedDelegate> Delegates { get; private set; }
        public bool isDelegate { get; private set; }
        public int DelegateId { get; private set; }
        private Random rnd = new Random(new Random(DateTime.Now.Millisecond).Next());
        public bool usePacketQueue { get; private set; }
        public bool useUdp { get; private set; }
        public bool NoExceptions { get; private set; }
        public bool MultiThreaded { get; private set; }

        public uint TimeOutLength { get; private set; }
        public object TimeOutValue { get; private set; }

        [NonSerialized]
        internal List<HookAttribute> Hooks;

        [NonSerialized]
        public Func<Object, Object[], Object> CallCache;

        internal SharedMethod(MethodInfo info, SharedClass sharedClass, bool isDelegate = false, int DelegateId = 0)
        {
            this.Name = info.Name;
            this.DelegateIndex = new SortedList<int, SharedDelegateInfo>();
            this.Delegates = new SortedList<int, SharedDelegate>();
            this.Hooks = new List<HookAttribute>();

            ParameterInfo[] parameters = info.GetParameters();
            List<Type> types = new List<Type>();
            for (int i = 0; i < parameters.Length; i++)
            {
                types.Add(parameters[i].ParameterType);

                if (parameters[i].ParameterType.BaseType != null)
                {
                    if (parameters[i].ParameterType.BaseType.FullName == "System.MulticastDelegate")
                    {
                        DelegateIndex.Add(i, new SharedDelegateInfo()
                        {
                            isUnchecked = (parameters[i].GetCustomAttributes(typeof(UncheckedRemoteExecutionAttribute), false).Length > 0),
                            UsePacketQueue = (parameters[i].GetCustomAttributes(typeof(PacketQueueAttribute), false).Length > 0),
                            UseUDP = (parameters[i].GetCustomAttributes(typeof(UdpMethodAttribute), false).Length > 0),
                            MultiThreaded = (parameters[i].GetCustomAttributes(typeof(MultiThreadAttribute), false).Length > 0)
                        });
                    }
                }
            }

            this.ArgumentTypes = types.ToArray();
            this.ReturnType = info.ReturnType;
            this.Unchecked = (info.GetCustomAttributes(typeof(UncheckedRemoteExecutionAttribute), false).Length > 0);
            this.usePacketQueue = (info.GetCustomAttributes(typeof(PacketQueueAttribute), false).Length > 0);
            this.useUdp = (info.GetCustomAttributes(typeof(UdpMethodAttribute), false).Length > 0);
            this.NoExceptions = (info.GetCustomAttributes(typeof(NoExceptionAttribute), false).Length > 0);
            this.MultiThreaded = (info.GetCustomAttributes(typeof(MultiThreadAttribute), false).Length > 0);

            object[] tempAttr = info.GetCustomAttributes(typeof(RemoteExecutionAttribute), false);
            if (tempAttr.Length > 0)
            {
                TimeOutLength = (tempAttr[0] as RemoteExecutionAttribute).TimeOut;
                TimeOutValue = (tempAttr[0] as RemoteExecutionAttribute).TimeOutValue;
            }

            foreach (object hookAttribute in info.GetCustomAttributes(typeof(HookAttribute), true))
            {
                HookAttribute hook = Activator.CreateInstance(hookAttribute.GetType()) as HookAttribute;
                if (hook != null)
                {
                    Hooks.Add(hook);
                }
            }

            types.Clear();
            types = null;
            this.CanReturn = info.ReturnType.FullName != "System.Void";
            this.SharedClass = sharedClass;
            this.isDelegate = isDelegate;
            this.DelegateId = DelegateId;
        }

        private void InvokeTask(object[] args)
        {
            object obj = null;
            _Invoke(ref obj, args);
        }

        private void _Invoke(ref object RetObject, params object[] args)
        {
            if (args.Length < ArgumentTypes.Length) //check if a argument is using "params x[] args"
                throw new Exception("missing arguments");

            List<int> usedDelegates = new List<int>();
            PayloadWriter pw = new PayloadWriter();

            SmartSerializer serializer = new SmartSerializer();
            for (int i = 0; i < args.Length; i++)
            {
                object obj = ArgumentTypes[i].IsByRef ? null : args[i];

                if (DelegateIndex.ContainsKey(i))
                    obj = null;

                using (MemoryStream TempStream = new MemoryStream())
                {
                    ProtoBuf.Serializer.Serialize(TempStream, obj);
                    pw.WriteInteger((int)TempStream.Length);
                    pw.WriteBytes(TempStream.GetBuffer(), 0, (int)TempStream.Length);
                }
            }

            for (int i = 0; i < DelegateIndex.Count; i++)
            {
                Delegate del = args[DelegateIndex.Keys[i]] as Delegate;

                if (del != null)
                {
                    if (del.Method == null)
                        throw new Exception("Target delegate is NULL");

                    int id = rnd.Next();
                    while (Delegates.ContainsKey(id))
                        id = rnd.Next();

                    pw.WriteBool(true);
                    SharedDelegate sharedDel = new SharedDelegate(del.Method, SharedClass, del.GetType(), id, del, this.MethodId);
                    sharedDel.sharedMethod.Unchecked = this.Unchecked; //DelegateIndex.Values[i].isUnchecked;
                    sharedDel.sharedMethod.usePacketQueue = this.usePacketQueue;//DelegateIndex.Values[i].UsePacketQueue;
                    sharedDel.sharedMethod.useUdp = this.useUdp;//DelegateIndex.Values[i].UseUDP;
                    pw.WriteObject(sharedDel);

                    if (!isDelegate)
                    {
                        Delegates.Add(id, sharedDel);
                    }
                    continue;
                }
                pw.WriteBool(false);
            }

            try
            {
                if (Unchecked || useUdp)
                {
                    //just execute the method and don't wait for response
                    SharedClass.Client.Send(new MsgExecuteMethod(0, pw.ToByteArray(), false, SharedClass.SharedId, MethodId, this.DelegateId, this.SharedClass.SharedId));
                }
                else
                {
                    SyncObject syncObject = null;
                    SecureRandom rnd = new SecureRandom();
                    int RequestId = rnd.Next();
                    lock (SharedClass.Client.Requests)
                    {
                        while (SharedClass.Client.Requests.ContainsKey(RequestId))
                            RequestId = rnd.Next();
                        syncObject = new SyncObject(SharedClass.Client);
                        SharedClass.Client.Requests.Add(RequestId, syncObject);
                        SharedClass.Client.Send(new MsgExecuteMethod(RequestId, pw.ToByteArray(), true, SharedClass.SharedId, MethodId, this.DelegateId, this.SharedClass.SharedId));
                    }

                    MsgExecuteMethodResponse response = syncObject.Wait<MsgExecuteMethodResponse>(null, TimeOutLength);
                    
                    if(response != null)
                    {
                        RetObject = response.GetReturnResult(ReturnType);

                        if (syncObject.TimedOut || (RetObject as ReturnResult != null && (RetObject as ReturnResult).UseTimeoutValue))
                        {
                            //copying the object in memory, maybe a strange way todo it but it works
                            RetObject = new ReturnResult(serializer.Deserialize(serializer.Serialize(this.TimeOutValue)), false);
                        }
                    }
                }
            }
            catch
            {
                //client most likely disconnected and was unable to send the message
                RetObject = null;
            }

            /*if (callback != null)
            {
                sharedClass.connection.BeginSendRequest(pw, callback, true, this.usePacketQueue);
            }
            else
            {
                if (Unchecked || useUdp)
                {
                    //just don't wait till we received something back since it's a VOID anyway
                    sharedClass.connection.BeginSendRequest(pw, (object obj) => { }, false, this.usePacketQueue);
                }
                else
                {
                    RetObject = sharedClass.connection.SendRequest(pw, this.usePacketQueue);
                }
            }*/
            serializer = null;
        }

        public object Invoke(params object[] args)
        {
            if (SharedClass.IsDisposed)
                throw new Exception("The shared class is disposed");

            object obj = null;
            _Invoke(ref obj, args);
            return obj;
        }

        public override string ToString()
        {
            string str = ReturnType.Name + "  " + this.Name + "(";
            for (int i = 0; i < ArgumentTypes.Length; i++)
            {
                str += ArgumentTypes[i].Name;
                if (i + 1 < ArgumentTypes.Length)
                    str += ", ";
            }
            str += ")";
            return str;
        }

        public void Dispose()
        {
            Name = null;
            ArgumentTypes = null;
            ReturnType = null;
            SharedClass = null;
        }
    }
}
