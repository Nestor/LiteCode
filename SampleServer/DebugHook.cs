using LiteCode.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleServer
{
    public class DebugHook : HookAttribute
    {
        public DebugHook()
        {

        }

        public override void PreExecuteMethod(SecureSocketProtocol3.Network.OperationalSocket OpSocket, LiteCode.Shared.SharedMethod Method, ref object[] Arguments)
        {
            
        }

        public override void PostExecuteMethod(SecureSocketProtocol3.Network.OperationalSocket OpSocket, LiteCode.Shared.SharedMethod Method, object[] Arguments, object ReturnValue, int TrafficUsed)
        {
            string DebugLine = "Method: " + Method.Name;
            DebugLine += ", Return Value: " + (ReturnValue != null ? ReturnValue.ToString() : "NULL");
            DebugLine += ", Traffic Used: " + TrafficUsed + "Bytes";

            DebugLine += ", Arguments (" + (Arguments == null ? 0 : Arguments.Length) + ")";

            if (Arguments != null && Arguments.Length > 0)
            {
                DebugLine += ": \r\n";

                for (int i = 0; i < Arguments.Length; i++)
                {
                    DebugLine += "\t[" + i + "] - ";

                    if (Arguments[i] == null)
                    {
                        DebugLine += "NULL";
                    }
                    else
                    {
                        DebugLine += Arguments[i].ToString();
                    }
                }
            }
            Console.WriteLine(DebugLine);
        }
    }
}