namespace TnTSoftware.Cqrs.MiniProfiler
{
    using System;

    public class MiniProfilerSwitch<TUser>
    {
        public Func<TUser, bool> Condition { get; set; }
    }
}