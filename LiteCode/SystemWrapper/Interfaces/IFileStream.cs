using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32.SafeHandles;
using System.Security.AccessControl;
using System.IO;

namespace LiteCode.SystemWrapper.Interfaces
{
    public interface IFileStream
    {
        bool CanRead { get; }
        bool CanSeek { get; }
        bool CanWrite { get; }
        IntPtr Handle { get; }
        bool IsAsync { get; }
        long Length { get; }
        string Name { get; }
        long Position { get; set; }
        SafeFileHandle SafeFileHandle { get; }
        IAsyncResult BeginRead(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject);
        IAsyncResult BeginWrite(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject);
        int EndRead(IAsyncResult asyncResult);
        void EndWrite(IAsyncResult asyncResult);
        void Flush();
        FileSecurity GetAccessControl();
        void Lock(long position, long length);
        byte[] Read(byte[] array, int offset, int count);
        int ReadByte();
        long Seek(long offset, SeekOrigin origin);
        void SetAccessControl(FileSecurity fileSecurity);
        void SetLength(long value);
        void Unlock(long position, long length);
        void Write(byte[] array, int offset, int count);
        void WriteByte(byte value);
        void QuickWrite(byte[] array, int offset, int count);
        void Close();
    }
}