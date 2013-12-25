using System;
using System.Collections.Generic;
using System.Text;
using LiteCode.Server;
using LiteCode.SystemWrapper.Interfaces;
using System.Net.Sockets;

namespace LiteCode.SystemWrapper
{
    public class Wrapper
    {
        private ICursor_Static _cursor;
        public ICursor_Static Cursor
        {
            get
            {
                if (_cursor == null)
                    _cursor = client.aClient.GetSharedClass<ICursor_Static>(typeof(Lite_Static_Cursor));
                return _cursor;
            }
        }


        public Lite_Graphics_Static Graphics { get; private set; }
        public Lite_Process_Static Process { get; private set; }
        private Client client;
        internal Wrapper(Client client)
        {
            this.client = client;
            this.Graphics = new Lite_Graphics_Static(client);
            this.Process = new Lite_Process_Static(client);
        }

        public ISocket New_Socket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
        {
            return client.aClient.GetSharedClass<ISocket>(typeof(Lite_Socket), addressFamily, socketType, protocolType);
        }
    }
}