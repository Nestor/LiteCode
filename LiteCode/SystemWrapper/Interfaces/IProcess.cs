using System;
using System.Collections.Generic;
using System.Text;

namespace LiteCode.SystemWrapper.Interfaces
{
    public interface IProcess
    {
        int BasePriority { get; }
        bool EnableRaisingEvents { get; set; }
        int ExitCode { get; }
        DateTime ExitTime { get; }
        IntPtr Handle { get; }
        int HandleCount { get; }
        bool HasExited { get; }
        int Id { get; }
        string MachineName { get; }
        //ProcessModule MainModule { get; }
        IntPtr MainWindowHandle { get; }
        string MainWindowTitle { get; }
        IntPtr MaxWorkingSet { get; set; }
        IntPtr MinWorkingSet { get; set; }
        //ProcessModuleCollection Modules { get; }
        int NonpagedSystemMemorySize { get; }
        long NonpagedSystemMemorySize64 { get; }
        int PagedMemorySize { get; }
        long PagedMemorySize64 { get; }
        int PagedSystemMemorySize { get; }
        long PagedSystemMemorySize64 { get; }
        int PeakPagedMemorySize { get; }
        long PeakPagedMemorySize64 { get; }
        int PeakVirtualMemorySize { get; }
        long PeakVirtualMemorySize64 { get; }
        int PeakWorkingSet { get; }
        long PeakWorkingSet64 { get; }
        bool PriorityBoostEnabled { get; set; }
        //ProcessPriorityClass PriorityClass { get; set; }
        int PrivateMemorySize { get; }
        long PrivateMemorySize64 { get; }
        TimeSpan PrivilegedProcessorTime { get; }
        string ProcessName { get; }
        IntPtr ProcessorAffinity { get; set; }
        bool Responding { get; }
        int SessionId { get; }
        //StreamReader StandardError { get; }
        //StreamWriter StandardInput { get; }
        //StreamReader StandardOutput { get; }
        //ProcessStartInfo StartInfo { get; set; }
        DateTime StartTime { get; }
        //ISynchronizeInvoke SynchronizingObject { get; set; }
        //ProcessThreadCollection Threads { get; }
        TimeSpan TotalProcessorTime { get; }
        TimeSpan UserProcessorTime { get; }
        int VirtualMemorySize { get; }
        long VirtualMemorySize64 { get; }
        int WorkingSet { get; }
        long WorkingSet64 { get; }

        void BeginErrorReadLine();
        void BeginOutputReadLine();
        void CancelErrorRead();
        void CancelOutputRead();
        void Close();
        bool CloseMainWindow();
        void Kill();
        void Refresh();
        bool Start();
        void WaitForExit();
        bool WaitForExit(int milliseconds);
        bool WaitForInputIdle();
        bool WaitForInputIdle(int milliseconds);
    }
}