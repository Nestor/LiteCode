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
        void CallTest(int test);
        void CallTest(string test);
        void CallTest(object test);
        void CallTest(int[] test);

        string StringTest();
        int IntegerTest();
        byte[] ByteArrayTest();
        void SecretShit();
        void DelegateTest(DelegateTestCallback Delly);
        void SendByteArray(byte[] data);

        //int IntegerTestError();
    }
}
