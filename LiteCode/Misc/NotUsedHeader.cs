using ProtoBuf;
using SecureSocketProtocol3.Network.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteCode.Misc
{
    [ProtoContract]
    internal class NotUsedHeader : Header
    {
        public NotUsedHeader()
            : base()
        {

        }

        public override Version Version
        {
            get { return new Version(1, 0, 0, 1); }
        }

        public override string HeaderName
        {
            get { return "Test Header"; }
        }
    }
}
