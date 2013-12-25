using System;
using System.Collections.Generic;
using System.Text;
using LiteCode.Shared;
using System.Reflection;
using SecureSocketProtocol2.Network;

namespace LiteCode
{
    class R_SharedMethodInvoke
    {
        public object onRequest(AClient connection, PayloadReader pr)
        {
            ReturnResult result = new ReturnResult(null, false);
            bool isDelegate = false;
            bool ReadFullHeader = false;

            try
            {
                int SharedClassId = pr.ReadInteger();
                int MethodId = pr.ReadInteger();
                isDelegate = pr.ReadByte() == 1;
                int DelegateId = isDelegate ? pr.ReadInteger() : 0;
                int DelegateClassId = isDelegate ? pr.ReadInteger() : 0;
                ReadFullHeader = true;

                if (connection.RemoteSharedClasses.ContainsKey(SharedClassId) ||
                    (isDelegate && connection.LocalSharedClasses.ContainsKey(DelegateClassId)))
                {
                    SharedClass sClass = isDelegate ? connection.LocalSharedClasses[SharedClassId] : connection.RemoteSharedClasses[SharedClassId];
                    SharedMethod sharedMethod = sClass.GetMethod(MethodId);

                    if (sharedMethod != null)
                    {
                        List<object> args = new List<object>();
                        List<Type> types = new List<Type>();
                        SortedList<int, SharedDelegate> SharedDelegates = new SortedList<int, SharedDelegate>();
                        SmartSerializer serializer = new SmartSerializer();

                        lock(sharedMethod.Delegates)
                        {
                            if(sharedMethod.Delegates.ContainsKey(DelegateId))
                            {
                                for (int i = 0; i < sharedMethod.Delegates[DelegateId].sharedMethod.ArgumentTypes.Length; i++)
                                {
                                    args.Add(serializer.Deserialize(pr.ReadBytes(pr.ReadInteger())));
                                }
                            }
                            else
                            {
                                for (int i = 0; i < sharedMethod.ArgumentTypes.Length; i++)
                                {
                                    args.Add(serializer.Deserialize(pr.ReadBytes(pr.ReadInteger())));
                                }
                            }
                        }                        

                        if (!isDelegate) //atm no support yet for delegate inside another delegate
                        {
                            for (int i = 0; i < sharedMethod.DelegateIndex.Count; i++)
                            {
                                if (pr.ReadByte() == 1)
                                {
                                    SharedDelegate del = pr.ReadObject<SharedDelegate>();
                                    del.sharedMethod.sharedClass = sClass;
                                    args[sharedMethod.DelegateIndex.Keys[i]] = DynamicDelegateCreator.CreateDelegate(del);
                                    SharedDelegates.Add(del.sharedMethod.DelegateId, del);
                                }
                            }
                        }

                        if (isDelegate)
                        {
                            //only show the result and not the actual ReturnResult type
                            return sharedMethod.Delegates[DelegateId].Delegate.DynamicInvoke(args.ToArray());
                        }
                        else
                        {
                            MethodInfo m = sClass.InitializedClass.GetType().GetMethod(sharedMethod.Name, sharedMethod.ArgumentTypes);

                            if (m.GetCustomAttributes(typeof(RemoteExecutionAttribute), false).Length == 0 &&
                                m.GetCustomAttributes(typeof(UncheckedRemoteExecutionAttribute), false).Length == 0)
                            {
                                return null;
                            }
                            result.ReturnValue = m.Invoke(sClass.InitializedClass, args.ToArray());
                        }
                    }
                }
            } catch (Exception ex)
            {
                if (isDelegate && ReadFullHeader)
                    throw ex;

                result.exceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                result.ExceptionOccured = true;
            }
            return result;
        }
    }
}