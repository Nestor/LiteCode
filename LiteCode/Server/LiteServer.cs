using System;
using System.Collections.Generic;
using System.Text;
using SecureSocketProtocol2;

namespace LiteCode.Server
{
    public class LiteServer : SSPServer<Client>
    {
        private class ServersProps : ServerProperties
        {
            private ushort port;
            private LiteServer server;
            public bool AllowUdp { get { return false; } }
            public object[] BaseClientArguments { get { return new object[] { server }; } }
            public CompressionType Compression { get { return CompressionType.QuickLZ; } }
            public EncryptionType Encryption { get { return EncryptionType.Wop; } }
            public bool ForceServerSettings { get { return true; } }
            public string ListenIp { get { return "0.0.0.0"; } }
            public ushort ListenPort { get { return port; } }

            public ServersProps(ushort port)
            {
                this.port = port;
            }
        }

        public List<Client> Clients;
        public delegate void ConnectionCallback(Client client);
        private ConnectionCallback _clientDisconnected;
        public event Action<Client> onClientConnect;
        public event Action<Client> onClientDisconnect;
        private ServersProps serverProperties;

        /// <summary>
        /// Initialize a new object of LiteServer
        /// </summary>
        /// <param name="port">The port to listen at</param>
        public LiteServer(ushort port)
            : base(new ServersProps(port))
        {
            Clients = new List<Client>();
            this._clientDisconnected = ClientDisconnected;
        }

        private void ClientDisconnected(Client client)
        {
            lock (_clientDisconnected)
            {
                lock (Clients)
                {
                    if (Clients.Contains(client))
                    {
                        if (onClientDisconnect != null)
                            onClientDisconnect(client);

                        Clients.Remove(client);
                    }
                }
            }
        }

        public override void onConnectionAccept(Client client)
        {
            lock (Clients)
            {
                client.disconnectCallback = _clientDisconnected;
                Clients.Add(client);

                try
                {
                    if (onClientConnect != null)
                        onClientConnect(client);
                } catch { }
            }
        }

        public override void onConnectionClosed(Client client)
        {

        }
    }
}