using SecureSocketProtocol2.Network.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiteCode.Messages
{
    internal class MsgReleaseLock : IMessage
    {
        public uint LockId;
        public MsgReleaseLock(uint LockId)
            : base()
        {
            this.LockId = LockId;
        }
        public MsgReleaseLock()
            : base()
        {

        }
    }
}