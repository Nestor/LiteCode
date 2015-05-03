using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteCode.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class ExtensionAttribute : Attribute
    {

    }
}
