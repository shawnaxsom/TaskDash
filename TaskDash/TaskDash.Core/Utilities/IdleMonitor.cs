using System;
using System.Runtime.InteropServices;

namespace TaskDash.Core.Utilities
{
    internal struct LASTINPUTINFO
    {
        public uint cbSize;

        public uint dwTime;
    }

    /// <summary>
    /// Summary description for Win32.
    /// </summary>
    public class IdleMonitor
    {
        public const int IDLE_TIMEOUT = 15000; // 15 seconds
        public const int WAITING_FOR_INPUT_TIMEOUT = 4000; // 4 seconds

        public static bool IsIdle
        {
            get { return GetIdleTime() > IDLE_TIMEOUT; }
        }

        public static bool IsWaitingForInput
        {
            get { return GetIdleTime() > WAITING_FOR_INPUT_TIMEOUT; }
        }

        [DllImport("User32.dll")]
        public static extern bool LockWorkStation();

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("Kernel32.dll")]
        private static extern uint GetLastError();

        public static uint GetIdleTime()
        {
            var lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint) Marshal.SizeOf(lastInPut);
            GetLastInputInfo(ref lastInPut);

            return ((uint) Environment.TickCount - lastInPut.dwTime);
        }

        public static long GetTickCount()
        {
            return Environment.TickCount;
        }

        public static long GetLastInputTime()
        {
            var lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint) Marshal.SizeOf(lastInPut);
            if (!GetLastInputInfo(ref lastInPut))
            {
                throw new Exception(GetLastError().ToString());
            }

            return lastInPut.dwTime;
        }
    }
}