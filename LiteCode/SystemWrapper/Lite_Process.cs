using System;
using System.Collections.Generic;
using System.Text;
using LiteCode.SystemWrapper.Interfaces;
using System.Diagnostics;

namespace LiteCode.SystemWrapper
{
    public class Lite_Process : IProcess
    {
        private Process _process;
        public Lite_Process(ProcessInit initType)
        {
            switch (initType)
            {
                case ProcessInit.CurrentProcess:
                    this._process = Process.GetCurrentProcess();
                    break;
            }
        }

        public int BasePriority
        {
            [RemoteExecution]
            get { return _process.BasePriority; }
        }

        public bool EnableRaisingEvents
        {
            [RemoteExecution]
            get { return _process.EnableRaisingEvents; }
            [RemoteExecution]
            set { _process.EnableRaisingEvents = value; }
        }

        public int ExitCode
        {
            [RemoteExecution]
            get { return _process.ExitCode; }
        }

        public DateTime ExitTime
        {
            [RemoteExecution]
            get { return _process.ExitTime; }
        }

        public IntPtr Handle
        {
            [RemoteExecution]
            get { return _process.Handle; }
        }

        public int HandleCount
        {
            [RemoteExecution]
            get { return _process.HandleCount; }
        }

        public bool HasExited
        {
            [RemoteExecution]
            get { return _process.HasExited; }
        }

        public int Id
        {
            [RemoteExecution]
            get { return _process.Id; }
        }

        public string MachineName
        {
            [RemoteExecution]
            get { return _process.MachineName; }
        }

        public IntPtr MainWindowHandle
        {
            [RemoteExecution]
            get { return _process.MainWindowHandle; }
        }

        public string MainWindowTitle
        {
            [RemoteExecution]
            get { return _process.MainWindowTitle; }
        }

        public IntPtr MaxWorkingSet
        {
            [RemoteExecution]
            get { return _process.MaxWorkingSet; }
            [RemoteExecution]
            set { _process.MaxWorkingSet = value; }
        }

        public IntPtr MinWorkingSet
        {
            [RemoteExecution]
            get { return _process.MinWorkingSet; }
            [RemoteExecution]
            set { _process.MinWorkingSet = value; }
        }

        public int NonpagedSystemMemorySize
        {
            [RemoteExecution]
            get { return _process.NonpagedSystemMemorySize; }
        }

        public long NonpagedSystemMemorySize64
        {
            [RemoteExecution]
            get { return _process.NonpagedSystemMemorySize64; }
        }

        public int PagedMemorySize
        {
            [RemoteExecution]
            get { return _process.PagedMemorySize; }
        }

        public long PagedMemorySize64
        {
            [RemoteExecution]
            get { return _process.PagedMemorySize64; }
        }

        public int PagedSystemMemorySize
        {
            [RemoteExecution]
            get { return _process.PagedSystemMemorySize; }
        }

        public long PagedSystemMemorySize64
        {
            [RemoteExecution]
            get { return _process.PagedSystemMemorySize64; }
        }

        public int PeakPagedMemorySize
        {
            [RemoteExecution]
            get { return _process.PeakPagedMemorySize; }
        }

        public long PeakPagedMemorySize64
        {
            [RemoteExecution]
            get { return _process.PeakPagedMemorySize64; }
        }

        public int PeakVirtualMemorySize
        {
            [RemoteExecution]
            get { return _process.PeakVirtualMemorySize; }
        }

        public long PeakVirtualMemorySize64
        {
            [RemoteExecution]
            get { return _process.PeakVirtualMemorySize64; }
        }

        public int PeakWorkingSet
        {
            [RemoteExecution]
            get { return _process.PeakWorkingSet; }
        }

        public long PeakWorkingSet64
        {
            [RemoteExecution]
            get { return _process.PeakWorkingSet64; }
        }

        public bool PriorityBoostEnabled
        {
            [RemoteExecution]
            get { return _process.PriorityBoostEnabled; }
            [RemoteExecution]
            set { _process.PriorityBoostEnabled = value; }
        }

        public int PrivateMemorySize
        {
            [RemoteExecution]
            get { return _process.PrivateMemorySize; }
        }

        public long PrivateMemorySize64
        {
            [RemoteExecution]
            get { return _process.PrivateMemorySize64; }
        }

        public TimeSpan PrivilegedProcessorTime
        {
            [RemoteExecution]
            get { return _process.PrivilegedProcessorTime; }
        }

        public string ProcessName
        {
            [RemoteExecution]
            get { return _process.ProcessName; }
        }

        public IntPtr ProcessorAffinity
        {
            [RemoteExecution]
            get { return _process.ProcessorAffinity; }
            [RemoteExecution]
            set { _process.ProcessorAffinity = value; }
        }

        public bool Responding
        {
            [RemoteExecution]
            get { return _process.Responding; }
        }

        public int SessionId
        {
            [RemoteExecution]
            get { return _process.SessionId; }
        }

        public DateTime StartTime
        {
            [RemoteExecution]
            get { return _process.StartTime; }
        }

        public TimeSpan TotalProcessorTime
        {
            [RemoteExecution]
            get { return _process.TotalProcessorTime; }
        }

        public TimeSpan UserProcessorTime
        {
            [RemoteExecution]
            get { return _process.UserProcessorTime; }
        }

        public int VirtualMemorySize
        {
            [RemoteExecution]
            get { return _process.VirtualMemorySize; }
        }

        public long VirtualMemorySize64
        {
            [RemoteExecution]
            get { return _process.VirtualMemorySize64; }
        }

        public int WorkingSet
        {
            [RemoteExecution]
            get { return _process.WorkingSet; }
        }

        public long WorkingSet64
        {
            [RemoteExecution]
            get { return _process.WorkingSet64; }
        }

        [RemoteExecution]
        public void BeginErrorReadLine()
        {
            _process.BeginErrorReadLine();
        }

        [RemoteExecution]
        public void BeginOutputReadLine()
        {
            _process.BeginOutputReadLine();
        }

        [RemoteExecution]
        public void CancelErrorRead()
        {
            _process.CancelErrorRead();
        }

        [RemoteExecution]
        public void CancelOutputRead()
        {
            _process.CancelOutputRead();
        }

        [RemoteExecution]
        public void Close()
        {
            _process.Close();
        }

        [RemoteExecution]
        public bool CloseMainWindow()
        {
            return _process.CloseMainWindow();
        }

        [RemoteExecution]
        public void Kill()
        {
            _process.Kill();
        }

        [RemoteExecution]
        public void Refresh()
        {
            _process.Refresh();
        }

        [RemoteExecution]
        public bool Start()
        {
            return _process.Start();
        }

        [RemoteExecution]
        public void WaitForExit()
        {
            _process.WaitForExit();
        }

        [RemoteExecution]
        public bool WaitForExit(int milliseconds)
        {
            return _process.WaitForExit(milliseconds);
        }

        [RemoteExecution]
        public bool WaitForInputIdle()
        {
            return _process.WaitForInputIdle();
        }

        [RemoteExecution]
        public bool WaitForInputIdle(int milliseconds)
        {
            return _process.WaitForInputIdle(milliseconds);
        }
    }
}
