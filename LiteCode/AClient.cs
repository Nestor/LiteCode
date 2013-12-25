using System;
using System.Collections.Generic;
using System.Text;
using LiteCode.Shared;
using SecureSocketProtocol2.Network;
using SecureSocketProtocol2;
using SecureSocketProtocol2.Hashers;
using System.Diagnostics;
using LiteCode.Messages;
using SecureSocketProtocol2.Network.Messages;

namespace LiteCode
{
    public class AClient
    {
        public SSPClient Connection {get; internal set; }
        internal object BeginRequestLock = new object();
        internal SortedList<int, Request> Requests;
        private SortedList<uint, SyncObject> NetworkLocks;
        internal SortedList<string, SharedClass> SharingClasses;
        internal SortedList<int, SharedClass> RemoteSharedClasses;
        internal SortedList<int, SharedClass> LocalSharedClasses;
        private SortedList<int, SyncObject> GetSharedClassRequests;
        internal SortedList<int, SyncObject> MethodRequests;
        private object RequestLock = new object();
        internal delegate void NetworkLockCallback();

        public string RemoteIp
        {
            get { return Connection.RemoteIp; }
        }

        public bool Connected
        {
            get
            {
                return Connection != null ? Connection.Connected : false;
            }
        }

        internal AClient(SSPClient client)
            : this()
        {
            this.Connection = client;
        }

        internal AClient()
        {
            Requests = new SortedList<int, Request>();
            NetworkLocks = new SortedList<uint, SyncObject>();
            SharingClasses = new SortedList<string, SharedClass>();
            RemoteSharedClasses = new SortedList<int, SharedClass>();
            LocalSharedClasses = new SortedList<int, SharedClass>();
            GetSharedClassRequests = new SortedList<int, SyncObject>();
            MethodRequests = new SortedList<int, SyncObject>();
        }

        internal void ProcessPacket(IMessage message)
        {
            /*if(message.GetType() == typeof(MsgRequest))
            {
                MsgRequest req = message as MsgRequest;
                object ret = null;

                switch (packetId)
                {
                    case PacketId.Shared_GetClass:
                        ret = new R_GetSharedClass().onRequest(this, pr);
                        break;
                    case PacketId.Shared_ExecuteMethod:
                        ret = new R_SharedMethodInvoke().onRequest(this, pr);
                        break;
                }

                if (SendResultBack)
                {
                    Connection.SendMessage(new MsgRequestResult(RequestId, ret));
                }
            }*/
            if (message.GetType() == typeof(MsgGetSharedClass))
            {
                MsgGetSharedClass req = message as MsgGetSharedClass;
                object ret = new R_GetSharedClass().onRequest(this, req);
                Connection.SendMessage(new MsgGetSharedClassResponse(req.RequestId, ret as ReturnResult));
            }
            else if (message.GetType() == typeof(MsgGetSharedClassResponse))
            {
                MsgGetSharedClassResponse response = message as MsgGetSharedClassResponse;
                lock (GetSharedClassRequests)
                {
                    if (GetSharedClassRequests.ContainsKey(response.RequestId))
                    {
                        GetSharedClassRequests[response.RequestId].Value = response.Result;
                        GetSharedClassRequests[response.RequestId].Pulse();
                    }
                }
            }
            else if (message.GetType() == typeof(MsgExecuteMethod))
            {
                MsgExecuteMethod req = message as MsgExecuteMethod;
                object ret = new R_SharedMethodInvoke().onRequest(this, new PayloadReader(req.Data));

                if (req.RequireResultBack)
                {
                    Connection.SendMessage(new MsgExecuteMethodResponse(req.RequestId, ret as ReturnResult));
                    //Connection.SendMessage(new MsgRequestResult(RequestId, ret));
                }
            }
            else if (message.GetType() == typeof(MsgExecuteMethodResponse))
            {
                lock (MethodRequests)
                {
                    MsgExecuteMethodResponse response = message as MsgExecuteMethodResponse;
                    if(MethodRequests.ContainsKey(response.RequestId))
                    {
                        MethodRequests[response.RequestId].Value = response.result;
                        MethodRequests[response.RequestId].Pulse();
                        MethodRequests.Remove(response.RequestId);
                    }
                }
            }
            /*else if (message.GetType() == typeof(MsgRequestResult))
            {
                lock (BeginRequestLock)
                {
                    MsgRequestResult result = message as MsgRequestResult;
                    if (Requests.ContainsKey(result.RequestId))
                    {
                        Request req = Requests[result.RequestId];
                        req.syncObject.Value = result.ReturnObject;
                        req.syncObject.Pulse();

                        if (req.Async)
                            req.Callback(req.syncObject.Value);
                        Requests.Remove(result.RequestId);
                    }
                }
            }*/
            else if (message.GetType() == typeof(MsgReleaseLock))
            {
                lock (NetworkLocks)
                {
                    MsgReleaseLock releaseLock = message as MsgReleaseLock;
                    uint LockId = releaseLock.LockId;
                    if (NetworkLocks.ContainsKey(LockId))
                    {
                        NetworkLocks[LockId].Pulse();
                        NetworkLocks.Remove(LockId);
                    }
                    else
                    {
                        NetworkLocks.Add(LockId, new SyncObject(this.Connection.Connection));
                        NetworkLocks[LockId].Pulse();
                    }
                }
            }
        }

        internal uint NameToId(string name)
        {
            return new SuperFastHashUInt16Hack().Hash(ASCIIEncoding.Unicode.GetBytes(name));
        }

        /// <summary>
        /// Create a network lock and wait till the other side will release the lock
        /// </summary>
        /// <param name="name">The name of the lock</param>
        /// <param name="del">A delegate to jump to when the lock is released</param>
        /// <param name="TimeOut">The time to wait until we release the lock, 0=infinite wait time</param>
        internal void Lock(string name, NetworkLockCallback del = null, uint TimeOut = 0)
        {
            uint id = NameToId(name);

            lock (NetworkLocks)
            {
                if (!NetworkLocks.ContainsKey(id))
                    NetworkLocks.Add(id, new SyncObject(Connection.Connection));
            }

            NetworkLocks[id].Wait<object>(TimeOut);

            //if dead-lock happens it should be here
            lock (NetworkLocks)
            {
                NetworkLocks.Remove(id);
            }

            if (del != null)
                del();
        }

        /// <summary>
        /// Release a network lock at the other client
        /// </summary>
        /// <param name="name">The name of the lock</param>
        internal void ReleaseLock(string name)
        {
            Connection.SendMessage(new MsgReleaseLock(NameToId(name)));
        }

        /// <summary>
        /// Send a requests to the destination and receive a object back
        /// </summary>
        /// <param name="data">The data to send with the request</param>
        /// <param name="Timeout">The TimeOut in milliseconds, 0=INFINITE</param>
        /// <param name="protocol">The network protocol to use</param>
        /// <returns>The object to receive</returns>
        /*internal object SendRequest(PayloadWriter data, bool usePacketQueue, uint Timeout = 0)
        {
            if (data == null)
                return null;
            if (data.Length == 0)
                return null;

            Random rnd = new Random(DateTime.Now.Millisecond);
            int RequestId = rnd.Next();
            Request req = null;

            lock (BeginRequestLock)
            {
                while (Requests.ContainsKey(RequestId))
                    RequestId = rnd.Next();
                req = new Request(Connection.Connection, RequestId, data.ToByteArray(), true);
                Requests.Add(RequestId, req);
            }

            Connection.SendMessage(new MsgRequest(req.RequestId, req.SendResultBack));

            PayloadWriter pw = new PayloadWriter();
            pw.WriteByte((byte)PacketType.Request);
            pw.WriteInteger(req.RequestId);
            pw.WriteBool(req.SendResultBack);
            pw.WriteBytes(req.PacketData);
            Connection.SendPacket(pw.ToByteArray(), 0, (int)pw.Length);

            object ret = req.syncObject.Wait<object>(Timeout);
            if (req.syncObject.TimedOut)
                ret = null;
            data = null;
            return ret;
        }

        /// <summary>
        /// Start a sync request and receive the object in the Callback, this improved version of the "SendRequest" method
        /// </summary>
        /// <param name="data">The data to send with the request</param>
        /// <param name="callback">The method where the EndSendRequest shall be called</param>
        /// <param name="protocol">The network protocol to use</param>
        internal void BeginSendRequest(PayloadWriter data, RequestCallback callback, bool SendResultBack, bool usePacketQueue)
        {
            lock (BeginRequestLock)
            {
                if (data == null)
                    throw new ArgumentNullException("data");
                if (callback == null)
                    throw new ArgumentNullException("callback");
                if (data.Length == 0)
                    throw new ArgumentNullException("Data is empty");

                MsgRequest request = new MsgRequest(0, SendResultBack);


                if (SendResultBack)
                {
                    Random rnd = new Random(DateTime.Now.Millisecond);
                    int RequestId = rnd.Next();
                    while (Requests.ContainsKey(RequestId))
                        RequestId = rnd.Next();

                    request.RequestId = RequestId;
                    Requests.Add(RequestId, new Request(Connection.Connection, RequestId, data.ToByteArray(), SendResultBack, callback));
                }
                
                pw.WriteBytes(data.GetBuffer(), 0, (int)data.Length);
                Stopwatch sw = Stopwatch.StartNew();
                //if (usePacketQueue)
                //    Connection.SendPacketQueue(pw.GetBuffer(), 0, (int)pw.Length);
                //else
                //    Connection.SendPacket(pw.GetBuffer(), 0, (int)pw.Length);
                sw.Stop();
                data = null;
            }
        }*/

        /// <summary> Share a class with other clients to use for remote code execution </summary>
        /// <param name="ClassType">The Class object to share with others</param>
        /// <param name="RemoteInitialize">False: The class will be initialized locally using the "ClassArgs" objects,
        ///                                True: The remote client will give the ClassArgs to use for initializing the object and will ignore the local argument objects</param>
        /// <param name="ClassArgs">The objects to initialize the class with</param>
        public void ShareClass(Type ClassType, bool RemoteInitialize = false, params object[] ClassArgs)
        {
            lock(SharingClasses)
            {
                if (SharingClasses.ContainsKey(ClassType.Name))
                    throw new Exception("A class with this name is already shared");
                SharingClasses.Add(ClassType.Name, new SharedClass(ClassType, this, RemoteInitialize, ClassArgs));
            }
        }

        /// <summary> Share a class with other clients to use for remote code execution </summary>
        /// <param name="Name">The name will be used to make a unique identifier for each shared class</param>
        /// <param name="ClassType">The Class object to share with others</param>
        /// <param name="RemoteInitialize">False: The class will be initialized locally using the "ClassArgs" objects,
        ///                                True: The remote client will give the ClassArgs to use for initializing the object and will ignore the local argument objects</param>
        /// <param name="ClassArgs">The objects to initialize the class with</param>
        public void ShareClass(string Name, Type ClassType, bool RemoteInitialize = false, params object[] ClassArgs)
        {
            lock(SharingClasses)
            {
                if (Name == null)
                    throw new ArgumentNullException("Name");
                if (SharingClasses.ContainsKey(Name))
                    throw new Exception("A class with this name is already shared");
                SharingClasses.Add(Name, new SharedClass(ClassType, this, RemoteInitialize, ClassArgs));
            }
        }

        /// <summary> Get the shared class from the remote client </summary>
        /// <param name="name">The name of the class that has been shared</param>
        /// <param name="BaseType"></param>
        /// <param name="RemoteArgs">Use this only if the shared class is having RemoteInitialize enabled</param>
        /// <returns>The shared class from the remote client</returns>
        public InterfacePrototype GetSharedClass<InterfacePrototype>(string name, params object[] RemoteArgs)
        {
            SyncObject syncObject = null;
            lock (GetSharedClassRequests)
            {
                int RequestId = 0;
                Random rnd = new Random(DateTime.Now.Millisecond);
                while (Requests.ContainsKey(RequestId))
                    RequestId = rnd.Next();
                syncObject = new SyncObject(Connection.Connection);
                GetSharedClassRequests.Add(RequestId, syncObject);

                Connection.SendMessage(new MsgGetSharedClass(name, RemoteArgs, RequestId)); //send message
            }

            ReturnResult result = syncObject.Wait<ReturnResult>(null, 0); //wait for response

            if(result == null)
                throw new Exception("A timeout occured");

            if (result.ExceptionOccured)
                throw new Exception(result.exceptionMessage);

            if (result.ReturnValue == null)
                throw new Exception("The shared class \"" + name + "\" could not be found in the remote client");

            SharedClass c = (SharedClass)result.ReturnValue;
            c.connection = this;
            InterfacePrototype tmp = DynamicClassCreator.CreateDynamicClass<InterfacePrototype>(c);

            lock(LocalSharedClasses)
            {
                LocalSharedClasses.Add(c.SharedId, c);
            }
            return tmp;
        }

        /// <summary> Get the shared class from the remote client </summary>
        /// <param name="name">The name of the class that has been shared</param>
        /// <param name="BaseType"></param>
        /// <param name="RemoteArgs">Use this only if the shared class is having RemoteInitialize enabled</param>
        /// <returns>The shared class from the remote client</returns>
        public InterfacePrototype GetSharedClass<InterfacePrototype>(params object[] RemoteArgs)
        {
            return GetSharedClass<InterfacePrototype>(typeof(InterfacePrototype).Name, RemoteArgs);
        }

        public void Disconnect()
        {
            Connection.Disconnect();
        }
    }
}