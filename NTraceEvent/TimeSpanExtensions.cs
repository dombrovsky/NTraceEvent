namespace NTraceEvent
{
    using System;

    internal static class TimeSpanExtensions
    {
        private const long TicksPerMicrosecond = TimeSpan.TicksPerMillisecond / 1000L;

        public static long TotalMicroseconds(this TimeSpan timeSpan)
        {
            return timeSpan.Ticks / TicksPerMicrosecond;
        }
    }
}