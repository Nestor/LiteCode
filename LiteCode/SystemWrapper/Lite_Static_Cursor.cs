using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LiteCode.SystemWrapper.Interfaces;

namespace LiteCode.SystemWrapper
{
    public class Lite_Static_Cursor : ICursor_Static
    {
        public System.Drawing.Point Position
        {
            [RemoteExecution]
            get { return Cursor.Position; }
            [RemoteExecution]
            set { Cursor.Position = value; }
        }

        public System.Drawing.Rectangle Clip
        {
            [RemoteExecution]
            get { return Cursor.Clip; }
            [RemoteExecution]
            set { Cursor.Clip = value; }
        }

        [RemoteExecution]
        public void Hide()
        {
            Cursor.Hide();
        }

        [RemoteExecution]
        public void Show()
        {
            Cursor.Show();
        }
    }
}