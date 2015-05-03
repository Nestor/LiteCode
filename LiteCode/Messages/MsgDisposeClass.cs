using LiteCode.Shared;
using ProtoBuf;
using SecureSocketProtocol3;
using SecureSocketProtocol3.Network;
using SecureSocketProtocol3.Network.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteCode.Messages
{
    [ProtoContract]
    internal class MsgDisposeClass : IMessage
    {
        [ProtoMember(1)]
        public int SharedClassId { get; set; }

        public MsgDisposeClass()
            : base()
        {

        }
        public MsgDisposeClass(int SharedClassId)
            : base()
        {
            this.SharedClassId = SharedClassId;
        }

        public override void ProcessPayload(SSPClient client, OperationalSocket OpSocket)
        {
            LiteCodeClient Client = OpSocket as LiteCodeClient;
            lock (Client.InitializedClasses)
            {
                if (Client.InitializedClasses.ContainsKey(SharedClassId))
                {
                    SharedClass initClass = Client.InitializedClasses[SharedClassId];
                    Client.InitializedClasses[SharedClassId].IsDisposed = true;

                    SharedClass localSharedClass = null;
                    lock (Client.SharedClasses)
                    {
                        if (Client.SharedClasses.TryGetValue(initClass.SharedName, out localSharedClass))
                        {
                            localSharedClass.SharedInitializeCounter--;
                        }
                    }

                    Client.InitializedClasses[SharedClassId] = null;
                    Client.InitializedClasses.Remove(SharedClassId);
                }
                else
                {
                    //strange client behavior
                    Client.Disconnect();
                }
            }
        }
    }
}
