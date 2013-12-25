using System;
using System.Collections.Generic;
using System.Text;
using LiteCode.Server;
using System.Diagnostics;
using LiteCode.SystemWrapper.Interfaces;

namespace LiteCode.SystemWrapper
{
    public class Lite_Process_Static
    {
        private Client client;
        internal Lite_Process_Static(Client client)
        {
            this.client = client;
        }

        public IProcess GetCurrentProcess()
        {
            return client.aClient.GetSharedClass<IProcess>(typeof(Lite_Process), ProcessInit.CurrentProcess);
        }
    }
}