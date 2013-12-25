using System;
using System.Collections.Generic;
using System.Text;
using LiteCode.SystemWrapper.Interfaces;
using System.IO;

namespace LiteCode.SystemWrapper
{
    public class Lite_FileStream : IFileStream
    {
        private FileStream stream;

        [RemoteConstructor]
        public Lite_FileStream(string path, FileMode mode, FileAccess access, FileShare share)
        {
            this.stream = new FileStream(path, mode, access, share);
        }

        public bool CanRead
        {
            [RemoteExecution]
            get { return stream.CanRead; }
        }

        public bool CanSeek
        {
            [RemoteExecution]
            get { return stream.CanSeek; }
        }

        public bool CanWrite
        {
            [RemoteExecution]
            get { return stream.CanWrite; }
        }

        public IntPtr Handle
        {
            [RemoteExecution]
            get { return stream.Handle; }
        }

        public bool IsAsync
        {
            [RemoteExecution]
            get { return stream.IsAsync; }
        }

        public long Length
        {
            [RemoteExecution]
            get { return stream.Length; }
        }

        public string Name
        {
            [RemoteExecution]
            get { return stream.Name; }
        }

        public long Position
        {
            [RemoteExecution]
            get { return stream.Position; }
            [RemoteExecution]
            set { stream.Position = value; }
        }
        
        public Microsoft.Win32.SafeHandles.SafeFileHandle SafeFileHandle
        {
            [RemoteExecution]
            get { return stream.SafeFileHandle; }
        }
        
        [RemoteExecution]
        public IAsyncResult BeginRead(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
        {
            return stream.BeginRead(array, offset, numBytes, userCallback, stateObject);
        }
        
        [RemoteExecution]
        public IAsyncResult BeginWrite(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
        {
            return stream.BeginWrite(array, offset, numBytes, userCallback, stateObject);
        }
        
        [RemoteExecution]
        public int EndRead(IAsyncResult asyncResult)
        {
            return stream.EndRead(asyncResult);
        }
        
        [RemoteExecution]
        public void EndWrite(IAsyncResult asyncResult)
        {
            stream.EndWrite(asyncResult);
        }
        
        [RemoteExecution]
        public void Flush()
        {
            stream.Flush();
        }
        
        [RemoteExecution]
        public System.Security.AccessControl.FileSecurity GetAccessControl()
        {
            return stream.GetAccessControl();
        }
        
        [RemoteExecution]
        public void Lock(long position, long length)
        {
            stream.Lock(position, length);
        }
        
        [RemoteExecution]
        public byte[] Read(byte[] array, int offset, int count)
        {
            int readed = stream.Read(array, offset, count);
            Array.Resize(ref array, readed);
            return array;
        }
        
        [RemoteExecution]
        public int ReadByte()
        {
            return stream.ReadByte();
        }
        
        [RemoteExecution]
        public long Seek(long offset, System.IO.SeekOrigin origin)
        {
            return stream.Seek(offset, origin);
        }
        
        [RemoteExecution]
        public void SetAccessControl(System.Security.AccessControl.FileSecurity fileSecurity)
        {
            stream.SetAccessControl(fileSecurity);
        }
        
        [RemoteExecution]
        public void SetLength(long value)
        {
            stream.SetLength(value);
        }
        
        [RemoteExecution]
        public void Unlock(long position, long length)
        {
            stream.Unlock(position, length);
        }

        [RemoteExecution]
        public void Write(byte[] array, int offset, int count)
        {
            stream.Write(array, offset, count);
        }

        /// <summary>
        /// Writes to the file without waiting from the remote machine if the data is written to disk 
        /// </summary>
        [UncheckedRemoteExecution]
        public void QuickWrite(byte[] array, int offset, int count)
        {
            stream.Write(array, offset, count);
        }
        
        [RemoteExecution]
        public void WriteByte(byte value)
        {
            stream.WriteByte(value);
        }

        [RemoteExecution]
        public void Close()
        {
            stream.Close();
        }
    }
}