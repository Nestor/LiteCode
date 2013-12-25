using System;
using System.Collections.Generic;
using System.Text;

namespace LiteCode
{
    public enum PacketId
    {
        Shared_GetClass,
        Shared_ExecuteMethod,
        SendPacket,
        TargetSendPacket
    }

    public enum GraphicsInit
    {
        FromHdc,
        FromHdcInternal,
        FromHwnd,
        FromHwndInternal
    }

    public enum ProcessInit
    {
        CurrentProcess,
        CurrentProcessById
    }
}