using System;
using System.Collections.Generic;
using System.Text;

namespace LiteCode
{
    /// <summary>
    /// Improves security of Shared Classes, preventing exploits
    /// </summary>
    public class RemoteExecutionAttribute : Attribute
    {
        public RemoteExecutionAttribute()
            : base()
        {

        }
    }
}