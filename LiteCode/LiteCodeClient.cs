using LiteCode.Messages;
using LiteCode.Misc;
using LiteCode.Shared;
using SecureSocketProtocol3;
using SecureSocketProtocol3.Network;
using SecureSocketProtocol3.Network.Headers;
using SecureSocketProtocol3.Network.Messages;
using SecureSocketProtocol3.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LiteCode
{
    public class LiteCodeClient : OperationalSocket
    {
        //LiteCode
        internal SortedList<decimal, SyncObject> Requests { get; private set; }
        internal SortedList<string, SharedClass> SharedClasses { get; private set; }
        internal SortedList<int, SharedClass> InitializedClasses { get; private set; }

        public LiteCodeClient(SSPClient client)
            : base(client)
        {

        }

        public override string Name
        {
            get { return "LiteCode"; }
        }

        public override Version Version
        {
            get { return new Version(1, 0); }
        }

        public override void onBeforeConnect()
        {
            Requests = new SortedList<decimal, SyncObject>();
            SharedClasses = new SortedList<string, SharedClass>();
            InitializedClasses = new SortedList<int, SharedClass>();

            base.Headers.RegisterHeader(typeof(NotUsedHeader));
            base.MessageHandler.AddMessage(typeof(MsgDisposeClass), "DISPOSE_CLASS");
            base.MessageHandler.AddMessage(typeof(MsgExecuteMethod), "EXECUTE_METHOD");
            base.MessageHandler.AddMessage(typeof(MsgExecuteMethodResponse), "EXECUTE_METHOD_RESPONSE");
            base.MessageHandler.AddMessage(typeof(MsgGetSharedClass), "GET_SHARED_CLASS");
            base.MessageHandler.AddMessage(typeof(MsgGetSharedClassResponse), "GET_SHARED_CLASS_RESPONSE");
        }

        public override void onConnect()
        {

        }

        public override void onDisconnect(SecureSocketProtocol3.DisconnectReason Reason)
        {

        }

        public override void onException(Exception ex, SecureSocketProtocol3.ErrorType errorType)
        {

        }

        public override void onReceiveMessage(IMessage Message, Header header)
        {
            Message.ProcessPayload(base.Client, this);
        }

        public int GetNextRandomInteger()
        {
            return base.Client.Connection.GetNextRandomInteger();
        }

        public void Send(IMessage message)
        {
            base.SendMessage(message, new NotUsedHeader());
        }

        /// <summary> Share a class with other clients to use for remote code execution </summary>
        /// <param name="ClassType">The Class object to share with others</param>
        /// <param name="RemoteInitialize">False: The class will be initialized locally using the "ClassArgs" objects,
        ///                                True: The remote client will give the ClassArgs to use for initializing the object and will ignore the local argument objects</param>
        /// <param name="ClassArgs">The objects to initialize the class with</param>
        /// <param name="MaxInitializations">The maximum count that the class can be shared </param>
        public void ShareClass(Type ClassType, bool RemoteInitialize = false, int MaxInitializations = 100, params object[] ClassArgs)
        {
            lock (SharedClasses)
            {
                if (SharedClasses.ContainsKey(ClassType.Name))
                    throw new Exception("A class with this name is already shared");
                SharedClasses.Add(ClassType.Name, new SharedClass(ClassType.Name, ClassType, this, RemoteInitialize, MaxInitializations, ClassArgs));
            }
        }

        /// <summary> Share a class with other clients to use for remote code execution </summary>
        /// <param name="Name">The name will be used to make a unique identifier for each shared class</param>
        /// <param name="ClassType">The Class object to share with others</param>
        /// <param name="RemoteInitialize">False: The class will be initialized locally using the "ClassArgs" objects,
        ///                                True: The remote client will give the ClassArgs to use for initializing the object and will ignore the local argument objects</param>
        /// <param name="ClassArgs">The objects to initialize the class with</param>
        /// <param name="MaxInitializations">The maximum count that the class can be shared </param>
        public void ShareClass(string Name, Type ClassType, bool RemoteInitialize = false, int MaxInitializations = 100, params object[] ClassArgs)
        {
            lock (SharedClasses)
            {
                if (Name == null)
                    throw new ArgumentNullException("Name");
                if (SharedClasses.ContainsKey(Name))
                    throw new Exception("A class with this name is already shared");
                SharedClasses.Add(Name, new SharedClass(Name, ClassType, this, RemoteInitialize, MaxInitializations, ClassArgs));
            }
        }

        public InterfacePrototype GetSharedClass<InterfacePrototype>(string name, params object[] RemoteArgs)
        {
            lock (InitializedClasses)
            {
                SyncObject syncObject = null;
                lock (Requests)
                {
                    int RequestId = 0;

                    RequestId = GetNextRandomInteger();
                    while (Requests.ContainsKey(RequestId))
                        RequestId = GetNextRandomInteger();

                    syncObject = new SyncObject(this);
                    Requests.Add(RequestId, syncObject);
                    Send(new MsgGetSharedClass(name, RemoteArgs, RequestId));
                }

                ReturnResult result = syncObject.Wait<ReturnResult>(null, 30000); //wait for response
                if (result == null)
                    throw new Exception("A timeout occured");
                if (result.ExceptionOccured)
                    throw new Exception(result.exceptionMessage);
                if (result.ReturnValue == null)
                    throw new Exception("The shared class \"" + name + "\" could not be found in the remote client");

                SharedClass c = (SharedClass)result.ReturnValue;
                c.Client = this;
                InterfacePrototype tmp = DynamicClassCreator.CreateDynamicClass<InterfacePrototype>(c);

                InitializedClasses.Add(c.SharedId, c);
                return tmp;
            }
        }

        public void DisposeSharedClass(object SharedClass)
        {
            lock (InitializedClasses)
            {
                if (SharedClass == null)
                    throw new ArgumentNullException("SharedClass");

                Type type = SharedClass.GetType();
                FieldInfo[] inf = type.GetFields();

                if (inf.Length == 0)
                    throw new Exception("Invalid Shared Class");

                SharedClass shared = inf[0].GetValue(SharedClass) as SharedClass;

                if (shared == null)
                    throw new Exception("Invalid Shared Class");

                if (shared.IsDisposed)
                    return;

                if (InitializedClasses.ContainsKey(shared.SharedId))
                {
                    InitializedClasses[shared.SharedId].IsDisposed = true;
                    InitializedClasses[shared.SharedId].InitializedClass = null;
                    InitializedClasses.Remove(shared.SharedId);
                }
                Send(new MsgDisposeClass(shared.SharedId));
            }
        }
    }
}