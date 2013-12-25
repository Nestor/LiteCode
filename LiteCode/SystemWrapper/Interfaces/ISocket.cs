using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace LiteCode.SystemWrapper.Interfaces
{
    public interface ISocket
    {
        int Available { get; }
        bool Blocking { get; set; }
        bool DontFragment { get; set; }
        bool EnableBroadcast { get; set; }
        bool ExclusiveAddressUse { get; set; }
        IntPtr Handle { get; }
        bool IsBound { get; }
        LingerOption LingerState { get; set; }
        EndPoint LocalEndPoint { get; }
        bool MulticastLoopback { get; set; }
        bool NoDelay { get; set; }
        ProtocolType ProtocolType { get; }
        int ReceiveBufferSize { get; set; }
        int ReceiveTimeout { get; set; }
        EndPoint RemoteEndPoint { get; }
        int SendBufferSize { get; set; }
        int SendTimeout { get; set; }
        SocketType SocketType { get; }
        short Ttl { get; set; }
        bool UseOnlyOverlappedIO { get; set; }
        bool Connected { get; }
        void Connect(string Host, int Port);
        int Send(byte[] buffer);
        int Send(byte[] buffer, SocketFlags socketFlags);
        int Send(byte[] buffer, int size, SocketFlags socketFlags);
        int Send(byte[] buffer, int offset, int size, SocketFlags socketFlags);
        byte[] Receive(int size);
        byte[] Receive(int size, SocketFlags socketFlags);
    }
}