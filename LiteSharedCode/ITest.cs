using System;
using System.Collections.Generic;
using System.Text;
using LiteCode;

namespace LiteSharedCode
{
    public delegate void TestDel(string ar);

    public interface ITest
    {
        string TestMe();
        string[] GetStrings();
        int GetRandomNumber();
        void TestDelegate(string test, [UncheckedRemoteExecution] TestDel CallMe);
        void OutTest(out int SomeValue);
        string PropertyTest { get; set; }
        void VoidTest();
    }
}
