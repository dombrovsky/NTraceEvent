namespace NTraceEvent
{
    using System;
    using System.Collections.Generic;

    internal interface ITraceEvent
    {
        TraceEventType Type { get; }

        string Name { get; }

        IReadOnlyCollection<string> Categories { get; }

        TimeSpan Timestamp { get; }

        TimeSpan? ThreadTimestamp { get; }

        int ThreadId { get; }

        int ProcessId { get; }

        ReservedColor Color { get; }
    }
}