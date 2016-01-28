﻿using LiteCode.Attributes;
using LiteCode.Shared;
using LiteCode.Misc;
using ProtoBuf;
using SecureSocketProtocol3;
using SecureSocketProtocol3.Network;
using SecureSocketProtocol3.Network.Messages;
using SecureSocketProtocol3.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SecureSocketProtocol3.Security.Serialization;

namespace LiteCode.Messages
{
    [ProtoContract]
    internal class MsgExecuteMethod : IMessage
    {
        [ProtoMember(1)]
        public byte[] ResultData { get; set; }

        [ProtoMember(2)]
        public int RequestId { get; set; }

        [ProtoMember(3)]
        public byte[] Data { get; set; }

        [ProtoMember(4)]
        public bool RequireResultBack { get; set; }

        [ProtoMember(5)]
        public int SharedClassId { get; set; }

        [ProtoMember(6)]
        public int MethodId { get; set; }

        [ProtoMember(7)]
        public int DelegateId { get; set; }

        [ProtoMember(8)]
        public int DelegateClassId { get; set; }

        bool isDelegate { get { return DelegateId > 0; } }

        public ReturnResult Result
        {
            get { return new ReturnResult().Deserialize(ResultData); }
            set { ResultData = value.Serialize(); }
        }

        public MsgExecuteMethod()
            : base()
        {

        }
        public MsgExecuteMethod(int RequestId, byte[] Data, bool RequireResultBack,
                                int SharedClassId, int MethodId, int DelegateId, int DelegateClassId)
            : base()
        {
            this.RequestId = RequestId;
            this.Data = Data;
            this.RequireResultBack = RequireResultBack;
            this.SharedClassId = SharedClassId;
            this.MethodId = MethodId;
            this.DelegateId = DelegateId;
            this.DelegateClassId = DelegateClassId;
        }
        public MsgExecuteMethod(int RequestId)
            : base()
        {
            this.RequestId = RequestId;
            this.Data = new byte[0];
        }

        public override void ProcessPayload(SSPClient client, OperationalSocket OpSocket)
        {
            ReturnResult result = new ReturnResult(null, false);
            LiteCodeClient Client = OpSocket as LiteCodeClient;
            SharedMethod sharedMethod = null;

            try
            {
                PayloadReader pr = new PayloadReader(Data);
                SharedClass sClass = null;

                if (Client.InitializedClasses.TryGetValue(SharedClassId, out sClass))
                {
                    sharedMethod = sClass.GetMethod(MethodId);

                    if (sharedMethod != null)
                    {
                        List<object> args = new List<object>();
                        List<Type> types = new List<Type>();
                        SortedList<int, SharedDelegate> SharedDelegates = new SortedList<int, SharedDelegate>();
                        SmartSerializer serializer = new SmartSerializer();

                        lock (sharedMethod.Delegates)
                        {
                            SharedDelegate sharedDel = null;
                            if (sharedMethod.Delegates.TryGetValue(DelegateId, out sharedDel))
                            {
                                for (int i = 0; i < sharedDel.sharedMethod.ArgumentTypes.Length; i++)
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
                            result.ReturnValue = sharedMethod.Delegates[DelegateId].Delegate.DynamicInvoke(args.ToArray());
                        }
                        else
                        {
                            if (sharedMethod.CallCache == null)
                            {
                                MethodInfo methodInf = sClass.InitializedClass.GetType().GetMethod(sharedMethod.Name, sharedMethod.ArgumentTypes);
                                sharedMethod.CallCache = methodInf.Bind();
                            }
                            result.ReturnValue = sharedMethod.CallCache(sClass.InitializedClass, args.ToArray());

                            /*MethodInfo methodInf = sClass.InitializedClass.GetType().GetMethod(sharedMethod.Name, sharedMethod.ArgumentTypes);
                            result.ReturnValue = methodInf.Invoke(sClass.InitializedClass, args.ToArray());*/
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.exceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                result.ExceptionOccured = true;
                client.onException(ex.InnerException != null ? ex.InnerException : ex, ErrorType.UserLand);

                if (sharedMethod != null && sharedMethod.NoExceptions)
                {
                    result.exceptionMessage = null;
                    result.ExceptionOccured = false;
                    result.ReturnValue = null;
                    result.UseTimeoutValue = true;
                }
            }

            if (RequireResultBack)
            {
                Client.Send(new MsgExecuteMethodResponse(RequestId, result));
            }
        }
    }
}
