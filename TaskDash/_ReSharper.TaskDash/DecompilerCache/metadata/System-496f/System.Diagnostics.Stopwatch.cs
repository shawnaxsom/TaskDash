// Type: System.Diagnostics.Stopwatch
// Assembly: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.dll

using System;
using System.Runtime;

namespace System.Diagnostics
{
    public class Stopwatch
    {
        public static readonly long Frequency;
        public static readonly bool IsHighResolution;
        public Stopwatch();

        public bool IsRunning { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public TimeSpan Elapsed { get; }
        public long ElapsedMilliseconds { get; }

        public long ElapsedTicks { [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        get; }

        public void Start();
        public static Stopwatch StartNew();
        public void Stop();
        public void Reset();
        public void Restart();
        public static long GetTimestamp();
    }
}
