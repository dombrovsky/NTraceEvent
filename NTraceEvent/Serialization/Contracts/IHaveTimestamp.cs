namespace NTraceEvent
{
    using System;

    internal interface IHaveTimestamp
    {
        /// <summary>
        /// Gets the tracing clock timestamp of the event.
        /// </summary>
        TimeSpan Timestamp { get; }

        /// <summary>
        /// Gets the thread clock timestamp of the event.
        /// </summary>
        TimeSpan? ThreadTimestamp { get; }
    }
}