using System;
using System.Collections.Generic;
using System.Text;
using LiteCode.Shared;
using SecureSocketProtocol2.Network;
using LiteCode.Messages;

namespace LiteCode
{
    class R_GetSharedClass
    {
        public object onRequest(AClient connection, MsgGetSharedClass request)
        {
            ReturnResult result = new ReturnResult(null, false);

            lock(connection.SharingClasses)
            {
                try
                {
                    if (connection.SharingClasses.ContainsKey(request.ClassName))
                    {
                        SharedClass localSharedClass = connection.SharingClasses[request.ClassName];

                        if (localSharedClass.RemoteInitialize)
                        {
                            bool FoundConstructor = false;

                            if (request.ArgObjects.Length > 0)
                            {
                                //lets check if there is a constructor with these arguments
                                for (int i = 0; i < localSharedClass.ConstructorTypes.Count; i++)
                                {
                                    if (localSharedClass.ConstructorTypes[i].Length == request.ArgObjects.Length)
                                    {
                                        bool CorrectArgs = true;
                                        for (int j = 0; j < request.ArgObjects.Length; j++)
                                        {
                                            if (localSharedClass.ConstructorTypes[i][j] != request.ArgObjects[j].GetType() &&
                                                localSharedClass.ConstructorTypes[i][j] != request.ArgObjects[j].GetType().BaseType)
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
                                    return null;
                            }
                        }

                        SharedClass sClass = new SharedClass(localSharedClass.BaseClassType, connection, localSharedClass.RemoteInitialize, localSharedClass.BaseClassTypeArgs);
                        sClass.InitializedClass = Activator.CreateInstance(sClass.BaseClassType, localSharedClass.RemoteInitialize ? request.ArgObjects : sClass.BaseClassTypeArgs);
                        Random rnd = new Random(DateTime.Now.Millisecond);
                        int RandomId = rnd.Next();
                        while (connection.RemoteSharedClasses.ContainsKey(RandomId))
                            RandomId = rnd.Next();

                        sClass.SharedId = RandomId;
                        connection.RemoteSharedClasses.Add(RandomId, sClass);
                        result.ReturnValue = sClass;
                        return result;
                    }
                } catch (Exception ex)
                {
                    result.ExceptionOccured = true;
                    result.exceptionMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                }
            }
            return result;
        }
    }
}