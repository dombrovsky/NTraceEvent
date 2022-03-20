namespace NTraceEvent
{
    using System;

    internal interface ITraceEvent
    {
        /// <summary>
        /// Gets the event type.
        /// </summary>
        TraceEventType Type { get; }

        /// <summary>
        /// Gets the tracing clock timestamp of the event.
        /// </summary>
        TimeSpan Timestamp { get; }

        /// <summary>
        /// Gets the thread clock timestamp of the event.
        /// </summary>
        TimeSpan? ThreadTimestamp { get; }

        /// <summary>
        /// Gets the thread ID for the thread that output this event.
        /// </summary>
        int ThreadId { get; }

        /// <summary>
        /// Gets the process ID for the process that output this event.
        /// </summary>
        int ProcessId { get; }

        /// <summary>
        /// Gets a fixed color name to associate with the event. If provided, cname must be one of the names listed in <see cref="ReservedColor"/>.
        /// </summary>
        ReservedColor Color { get; }
    }
}