using System;
using System.Collections.Generic;
using System.Text;

namespace LiteCode
{
    /// <summary>
    /// This will sent the request to call the method without waiting till the request is being sent
    /// </summary>
    public class NoWaitMethodAttribute : Attribute
    {
        public NoWaitMethodAttribute()
            : base()
        {

        }
    }
}