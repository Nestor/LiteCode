using LiteCode.Shared;
using ProtoBuf;
using SecureSocketProtocol3.Network.Messages;
using SecureSocketProtocol3.Security.Serialization;
using SecureSocketProtocol3.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteCode.Messages
{
    [ProtoContract]
    internal class MsgGetSharedClass : IMessage
    {
        [ProtoMember(1)]
        public string ClassName { get; set; }

        [ProtoMember(2)]
        public byte[] ArgObjectsData { get; set; }

        [ProtoMember(3)]
        public int RequestId { get; set; }

        public object[] ArgObjects
        {
            get { return new SmartSerializer().Deserialize(ArgObjectsData) as object[]; }
            set { ArgObjectsData = new SmartSerializer().Serialize(value); }
        }

        public MsgGetSharedClass()
            : base()
        {

        }
        public MsgGetSharedClass(string ClassName, object[] ArgObjects, int RequestId)
            : base()
        {
            this.ClassName = ClassName;
            this.ArgObjects = ArgObjects;
            this.RequestId = RequestId;
        }

        public override void ProcessPayload(SecureSocketProtocol3.SSPClient client, SecureSocketProtocol3.Network.OperationalSocket OpSocket)
        {
            ReturnResult result = new ReturnResult(null, false);
            LiteCodeClient Client = OpSocket as LiteCodeClient;
            SharedClass localSharedClass = null;

            lock (Client.SharedClasses)
            {
                if (!Client.SharedClasses.TryGetValue(ClassName, out localSharedClass))
                {
                    result.ExceptionOccured = true;
                    result.exceptionMessage = "Shared Class not found";
                    result.ReturnValue = null;
                    Client.Send(new MsgGetSharedClassResponse(RequestId, result));
                    return;
                }
            }

            try
            {
                if (localSharedClass.RemoteInitialize)
                {
                    bool FoundConstructor = false;

                    if (ArgObjects.Length > 0)
                    {
                        //lets check if there is a constructor with these arguments
                        for (int i = 0; i < localSharedClass.ConstructorTypes.Count; i++)
                        {
                            if (localSharedClass.ConstructorTypes[i].Length == ArgObjects.Length)
                            {
                                bool CorrectArgs = true;
                                for (int j = 0; j < ArgObjects.Length; j++)
                                {
                                    if (localSharedClass.ConstructorTypes[i][j] != ArgObjects[j].GetType() &&
                                        localSharedClass.ConstructorTypes[i][j] != ArgObjects[j].GetType().BaseType)
                                    {
                                        CorrectArgs = false;
                                        break;
                                    }
                                }

                                if (CorrectArgs)
                                {
                                    FoundConstructor = true;
                                    break;
                                }
                            }
                        }
                        if (!FoundConstructor)
                            return;
                    }
                }

                if (localSharedClass.SharedInitializeCounter >= localSharedClass.MaxInitializations)
                {
                    result.ExceptionOccured = true;
                    result.exceptionMessage = "Reached maximum initializations";
                }
                else
                {
                    SharedClass sClass = new SharedClass(ClassName, localSharedClass.BaseClassType, Client, localSharedClass.RemoteInitialize, localSharedClass.MaxInitializations, localSharedClass.BaseClassTypeArgs);
                    sClass.InitializedClass = Activator.CreateInstance(sClass.BaseClassType, localSharedClass.RemoteInitialize ? ArgObjects : sClass.BaseClassTypeArgs);

                    int RandomId = Client.GetNextRandomInteger();
                    while (Client.InitializedClasses.ContainsKey(RandomId))
                        RandomId = Client.GetNextRandomInteger();

                    sClass.SharedId = RandomId;
                    Client.InitializedClasses.Add(RandomId, sClass);
                    result.ReturnValue = sClass;

                    localSharedClass.SharedInitializeCounter++;
                }
            }
            catch (Exception ex)
            {
                result.ExceptionOccured = true;
                result.exceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            Client.Send(new MsgGetSharedClassResponse(RequestId, result));
        }
    }
}
