using System;
using System.Collections.Generic;
using System.Text;

namespace LiteCode.Shared
{
    [Serializable]
    public class SharedDelegateInfo
    {
        public bool isUnchecked;
        public bool UseUDP;
        public bool UsePacketQueue;
        public bool NoWaitingTime;
    }
}