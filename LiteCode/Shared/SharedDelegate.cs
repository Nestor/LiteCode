using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace LiteCode.Shared
{
    [Serializable()]
    public class SharedDelegate
    {
        public SharedMethod sharedMethod;
        public Type DelegateType;
        [NonSerialized]
        public Delegate Delegate;

        internal SharedDelegate(MethodInfo info, SharedClass sharedClass, Type DelegateType, int DelegateId, Delegate Delegate, int MethodId)
        {
            this.sharedMethod = new SharedMethod(info, sharedClass, true, DelegateId);
            this.DelegateType = DelegateType;
            this.Delegate = Delegate;
            this.sharedMethod.MethodId = MethodId;
        }

        public object Invoke(params object[] args)
        {
            return sharedMethod.Invoke(args);
        }
    }
}