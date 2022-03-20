namespace NTraceEvent
{
    internal interface ITraceEvent
    {
        /// <summary>
        /// Gets the event type.
        /// </summary>
        TraceEventType Type { get; }

        /// <summary>
        /// Gets the thread ID for the thread that output this event.
        /// </summary>
        int ThreadId { get; }

        /// <summary>
        /// Gets the process ID for the process that output this event.
        /// </summary>
        int ProcessId { get; }
    }
}