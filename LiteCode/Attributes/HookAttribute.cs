using LiteCode.Shared;
using SecureSocketProtocol3.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteCode.Attributes
{
    public abstract class HookAttribute : Attribute
    {
        public HookAttribute()
        {

        }

        public abstract void PreExecuteMethod(OperationalSocket OpSocket, SharedMethod Method, ref object[] Arguments);
        public abstract void PostExecuteMethod(OperationalSocket OpSocket, SharedMethod Method, object[] Arguments, object ReturnValue, int TrafficUsed);
    }
}