using LiteCode.Attributes;
using LiteCode.Shared;
using LiteCode.Misc;
using ProtoBuf;
using SecureSocketProtocol3;
using SecureSocketProtocol3.Network;
using SecureSocketProtocol3.Network.Messages;
using SecureSocketProtocol3.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SecureSocketProtocol3.Security.Serialization;
using System.IO;
using System.Threading;

namespace LiteCode.Messages
{
    [ProtoContract]
    internal class MsgExecuteMethod : IMessage
    {
        [ProtoMember(1)]
        public int RequestId { get; set; }

        [ProtoMember(2)]
        public byte[] Data { get; set; }

        [ProtoMember(3)]
        public bool RequireResultBack { get; set; }

        [ProtoMember(4)]
        public int SharedClassId { get; set; }

        [ProtoMember(5)]
        public int MethodId { get; set; }

        [ProtoMember(6)]
        public int DelegateId { get; set; }

        [ProtoMember(7)]
        public int DelegateClassId { get; set; }

        bool isDelegate { get { return DelegateId > 0; } }
        private SharedMethod sharedMethod = null;
        private object[] UsedArguments = null;

        public MsgExecuteMethod()
            : base()
        {

        }
        public MsgExecuteMethod(int RequestId, byte[] Data, bool RequireResultBack,
                                int SharedClassId, int MethodId, int DelegateId, int DelegateClassId)
            : base()
        {
            this.RequestId = RequestId;
            this.Data = Data;
            this.RequireResultBack = RequireResultBack;
            this.SharedClassId = SharedClassId;
            this.MethodId = MethodId;
            this.DelegateId = DelegateId;
            this.DelegateClassId = DelegateClassId;
        }
        public MsgExecuteMethod(int RequestId)
            : base()
        {
            this.RequestId = RequestId;
            this.Data = new byte[0];
        }

        public override void ProcessPayload(SSPClient client, OperationalSocket OpSocket)
        {
            ReturnResult result = new ReturnResult(null, false);
            LiteCodeClient Client = OpSocket as LiteCodeClient;
            
            try
            {
                PayloadReader pr = new PayloadReader(Data);
                SharedClass sClass = null;

                if (Client.InitializedClasses.TryGetValue(SharedClassId, out sClass))
                {
                    sharedMethod = sClass.GetMethod(MethodId);
                    
                    if (sharedMethod != null)
                    {
                        List<object> args = new List<object>();
                        List<Type> types = new List<Type>();
                        SortedList<int, SharedDelegate> SharedDelegates = new SortedList<int, SharedDelegate>();
                        SmartSerializer serializer = new SmartSerializer();

                        lock (sharedMethod.Delegates)
                        {
                            SharedDelegate sharedDel = null;

                            if (sharedMethod.Delegates.TryGetValue(DelegateId, out sharedDel))
                            {
                                for (int i = 0; i < sharedDel.sharedMethod.ArgumentTypes.Length; i++)
                                {
                                    int length = pr.ReadInteger();

                                    if (length == 0)
                                    {
                                        args.Add(null);
                                        continue;
                                    }

                                    args.Add(Serializer.Deserialize(new MemoryStream(pr.Buffer, pr.Position, length), sharedDel.sharedMethod.ArgumentTypes[i]));
                                    pr.Position += length;
                                }
                            }
                            else
                            {
                                for (int i = 0; i < sharedMethod.ArgumentTypes.Length; i++)
                                {
                                    int length = pr.ReadInteger();

                                    if (length == 0)
                                    {
                                        args.Add(null);
                                        continue;
                                    }

                                    args.Add(Serializer.Deserialize(new MemoryStream(pr.Buffer, pr.Position, length), sharedMethod.ArgumentTypes[i]));
                                    pr.Position += length;
                                }
                            }
                        }

                        if (!isDelegate) //atm no support yet for delegate inside another delegate
                        {
                            for (int i = 0; i < sharedMethod.DelegateIndex.Count; i++)
                            {
                                if (pr.ReadByte() == 1)
                                {
                                    SharedDelegate del = pr.ReadObject<SharedDelegate>();
                                    del.sharedMethod.SharedClass = sClass;
                                    args[sharedMethod.DelegateIndex.Keys[i]] = DynamicDelegateCreator.CreateDelegate(del);
                                    SharedDelegates.Add(del.sharedMethod.DelegateId, del);
                                }
                            }
                        }

                        if (isDelegate)
                        {
                            result.ReturnValue = sharedMethod.Delegates[DelegateId].Delegate.DynamicInvoke(args.ToArray());
                        }
                        else
                        {
                            if (sharedMethod.CallCache == null)
                            {
                                MethodInfo methodInf = sClass.InitializedClass.GetType().GetMethod(sharedMethod.Name, sharedMethod.ArgumentTypes);
                                sharedMethod.CallCache = methodInf.Bind();
                            }

                            UsedArguments = args.ToArray();

                            //pre-call the hooks that are in place
                            foreach (HookAttribute Hook in sharedMethod.Hooks)
                            {
                                Hook.PreExecuteMethod(OpSocket, sharedMethod, ref UsedArguments);
                            }

                            if (sharedMethod.MultiThreaded)
                            {
                                Thread TempThread = new Thread(new ThreadStart(() =>
                                {
                                    try
                                    {
                                        result.ReturnValue = sharedMethod.CallCache(sClass.InitializedClass, UsedArguments);
                                        EndMethod(Client, OpSocket, result);
                                    }
                                    catch (Exception ex)
                                    {
                                        ExceptionOccured(ex, result, client);
                                        try { EndMethod(Client, OpSocket, result); } catch { }
                                    }
                                }));
                                TempThread.Start();
                            }
                            else
                            {
                                result.ReturnValue = sharedMethod.CallCache(sClass.InitializedClass, UsedArguments);
                            }

                            /*MethodInfo methodInf = sClass.InitializedClass.GetType().GetMethod(sharedMethod.Name, sharedMethod.ArgumentTypes);
                            result.ReturnValue = methodInf.Invoke(sClass.InitializedClass, args.ToArray());*/
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionOccured(ex, result, client);
            }

            if (!sharedMethod.MultiThreaded)
            {
                EndMethod(Client, OpSocket, result);
            }
        }

        private void ExceptionOccured(Exception ex, ReturnResult result, SSPClient client)
        {
            result.exceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            result.ExceptionOccured = true;
            client.onException(ex.InnerException != null ? ex.InnerException : ex, ErrorType.UserLand);

            if (sharedMethod != null && sharedMethod.NoExceptions)
            {
                result.exceptionMessage = null;
                result.ExceptionOccured = false;
                result.ReturnValue = null;
                result.UseTimeoutValue = true;
            }
        }

        private void EndMethod(LiteCodeClient Client, OperationalSocket OpSocket, ReturnResult result)
        {
            int TrafficUsed = 0;
            if (RequireResultBack)
            {
                TrafficUsed = Client.Send(new MsgExecuteMethodResponse(RequestId, result));
            }

            if (sharedMethod != null && sharedMethod.Hooks != null)
            {
                //post-call the hooks that are in place
                foreach (HookAttribute Hook in sharedMethod.Hooks)
                {
                    Hook.PostExecuteMethod(OpSocket, sharedMethod, UsedArguments, result.ReturnValue, TrafficUsed);
                }
            }
        }
    }
}