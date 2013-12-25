using System;
using System.Collections.Generic;
using System.Text;
using LiteCode.SystemWrapper.Interfaces;
using System.Net.Sockets;

namespace LiteCode.SystemWrapper
{
    public class Lite_Socket : ISocket
    {
        private Socket socket;

        public bool Connected
        {
            [RemoteExecution]
            get { return socket.Connected; }
        }

        public Lite_Socket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
        {
            this.socket = new Socket(addressFamily, socketType, protocolType);
        }

        [RemoteExecution]
        public void Connect(string Host, int Port)
        {
            socket.Connect(Host, Port);
        }

        [RemoteExecution]
        public int Send(byte[] buffer)
        {
            return Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        [RemoteExecution]
        public int Send(byte[] buffer, SocketFlags socketFlags)
        {
            return Send(buffer, 0, buffer.Length, socketFlags);
        }

        [RemoteExecution]
        public int Send(byte[] buffer, int size, SocketFlags socketFlags)
        {
            return Send(buffer, 0, size, socketFlags);
        }

        [RemoteExecution]
        public int Send(byte[] buffer, int offset, int size, SocketFlags socketFlags)
        {
            try
            {
                return socket.Send(buffer, offset, size, socketFlags);
            } catch { return 0; }
        }

        [RemoteExecution]
        public byte[] Receive(int size)
        {

            return Receive(size, SocketFlags.None);
        }

        [RemoteExecution]
        public byte[] Receive(int size, SocketFlags socketFlags)
        {
            try
            {
                byte[] buffer = new byte[size];
                int recv = socket.Receive(buffer, size, socketFlags);
                Array.Resize(ref buffer, recv);
                return buffer;
            } catch { return new byte[0]; }
        }
        
        public int Available
        {
            [RemoteExecution]
            get { return socket.Available; }
        }

        public bool Blocking
        {
            [RemoteExecution]
            get { return socket.Blocking; }
            [RemoteExecution]
            set { socket.Blocking = value; }
        }

        public bool DontFragment
        {
            [RemoteExecution]
            get { return socket.DontFragment; }
            [RemoteExecution]
            set { socket.DontFragment = value; }
        }

        public bool EnableBroadcast
        {
            [RemoteExecution]
            get { return socket.EnableBroadcast; }
            [RemoteExecution]
            set { socket.EnableBroadcast = value; }
        }

        public bool ExclusiveAddressUse
        {
            [RemoteExecution]
            get { return socket.ExclusiveAddressUse; }
            [RemoteExecution]
            set { socket.ExclusiveAddressUse = value; }
        }

        public IntPtr Handle
        {
            [RemoteExecution]
            get { return socket.Handle; }
        }

        public bool IsBound
        {
            [RemoteExecution]
            get { return socket.IsBound; }
        }

        public LingerOption LingerState
        {
            [RemoteExecution]
            get { return socket.LingerState; }
            [RemoteExecution]
            set { socket.LingerState = value; }
        }

        public System.Net.EndPoint LocalEndPoint
        {
            [RemoteExecution]
            get { return socket.LocalEndPoint; }
        }

        public bool MulticastLoopback
        {
            [RemoteExecution]
            get { return socket.MulticastLoopback; }
            [RemoteExecution]
            set { socket.MulticastLoopback = value; }
        }

        public bool NoDelay
        {
            [RemoteExecution]
            get { return socket.NoDelay; }
            [RemoteExecution]
            set { socket.NoDelay = value; }
        }

        public ProtocolType ProtocolType
        {
            [RemoteExecution]
            get { return socket.ProtocolType; }
        }

        public int ReceiveBufferSize
        {
            [RemoteExecution]
            get { return socket.ReceiveBufferSize; }
            [RemoteExecution]
            set { socket.ReceiveBufferSize = value; }
        }

        public int ReceiveTimeout
        {
            [RemoteExecution]
            get { return socket.ReceiveTimeout; }
            [RemoteExecution]
            set { socket.ReceiveTimeout = value; }
        }

        public System.Net.EndPoint RemoteEndPoint
        {
            [RemoteExecution]
            get { return socket.RemoteEndPoint; }
        }

        public int SendBufferSize
        {
            [RemoteExecution]
            get { return socket.SendBufferSize; }
            [RemoteExecution]
            set { socket.SendBufferSize = value; }
        }

        public int SendTimeout
        {
            [RemoteExecution]
            get { return socket.SendTimeout; }
            [RemoteExecution]
            set { socket.SendTimeout = value; }
        }

        public SocketType SocketType
        {
            [RemoteExecution]
            get { return socket.SocketType; }
        }

        public short Ttl
        {
            [RemoteExecution]
            get { return socket.Ttl; }
            [RemoteExecution]
            set { socket.Ttl = value; }
        }

        public bool UseOnlyOverlappedIO
        {
            [RemoteExecution]
            get { return socket.UseOnlyOverlappedIO; }
            [RemoteExecution]
            set { socket.UseOnlyOverlappedIO = value; }
        }
    }
}