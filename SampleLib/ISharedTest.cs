using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleLib
{
    public delegate void DelegateTestCallback(string Value);

    public interface ISharedTest
    {
        void CallTest();
        string StringTest();
        int IntegerTest();
        byte[] ByteArrayTest();
        void SecretShit();
        void DelegateTest(DelegateTestCallback Delly);
        void SendByteArray(byte[] data);
    }
}
