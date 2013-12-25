using System;
using System.Collections.Generic;
using System.Text;
using LiteCode.Server;
using System.Drawing;

namespace LiteCode.SystemWrapper.Interfaces
{
    public class Lite_Graphics_Static
    {
        private Client client;
        internal Lite_Graphics_Static(Client client)
        {
            this.client = client;
        }

        public IGraphics FromHdc(IntPtr hdc)
        {
            return client.aClient.GetSharedClass<IGraphics>(typeof(Lite_Graphics), hdc, GraphicsInit.FromHdc);
        }
        public IGraphics FromHdc(IntPtr hdc, IntPtr hdevice)
        {
            return client.aClient.GetSharedClass<IGraphics>(typeof(Lite_Graphics), hdc, hdevice);
        }
        public IGraphics FromHdcInternal(IntPtr hdc)
        {
            return client.aClient.GetSharedClass<IGraphics>(typeof(Lite_Graphics), hdc, GraphicsInit.FromHdcInternal);
        }
        public IGraphics FromHwnd(IntPtr hwnd)
        {
            return client.aClient.GetSharedClass<IGraphics>(typeof(Lite_Graphics), hwnd, GraphicsInit.FromHwnd);
        }
        public IGraphics FromHwndInternal(IntPtr hwnd)
        {
            return client.aClient.GetSharedClass<IGraphics>(typeof(Lite_Graphics), hwnd, GraphicsInit.FromHwndInternal);
        }
        public IGraphics FromImage(Image image)
        {
            return client.aClient.GetSharedClass<IGraphics>(typeof(Lite_Graphics), image);
        }
    }
}