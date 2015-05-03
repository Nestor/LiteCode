using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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
            if (sharedMethod.sharedClass.IsDisposed)
                throw new Exception("The shared class is disposed");

            ReturnResult ret = sharedMethod.Invoke(args) as ReturnResult;

            if (ret != null)
            {
                if (ret.ExceptionOccured)
                    throw new Exception(ret.exceptionMessage);
                return ret.ReturnValue;
            }
            return null;
        }
    }
}
