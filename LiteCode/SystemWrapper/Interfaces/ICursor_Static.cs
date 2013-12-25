using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace LiteCode.SystemWrapper.Interfaces
{
    public interface ICursor_Static
    {
        Rectangle Clip { get; set; }
        Point Position { get; set; }
        void Hide();
        void Show();
    }
}