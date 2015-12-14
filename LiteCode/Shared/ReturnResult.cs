using SecureSocketProtocol3.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace LiteCode.Shared
{
    [Serializable()]
    internal class ReturnResult
    {
        public object ReturnValue;
        public string exceptionMessage;
        public bool ExceptionOccured;
        public bool UseTimeoutValue;

        public ReturnResult(object ReturnValue, bool ExceptionOccured, string exceptionMessage = "")
        {
            this.ReturnValue = ReturnValue;
            this.ExceptionOccured = ExceptionOccured;
            this.exceptionMessage = exceptionMessage;
        }

        public ReturnResult()
        {

        }

        public byte[] Serialize()
        {
            return new SmartSerializer().Serialize(this);
        }

        public ReturnResult Deserialize(byte[] Data)
        {
            ReturnResult obj = new SmartSerializer().Deserialize(Data) as ReturnResult;
            
            if(obj != null)
            {
                this.ReturnValue = obj.ReturnValue;
                this.exceptionMessage = obj.exceptionMessage;
                this.ExceptionOccured = obj.ExceptionOccured;
                this.UseTimeoutValue = obj.UseTimeoutValue;
            }
            return this;
        }
    }
}