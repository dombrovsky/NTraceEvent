namespace NTraceEvent
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public readonly record struct DurationBeginTraceEvent : ITraceEvent, IHaveArguments, IHaveStackFrames, ISerializableTraceEvent
    {
        public TraceEventType Type => TraceEventType.DurationBegin;

        public string Name { get; init; } = string.Empty;

        public IReadOnlyCollection<string> Categories { get; init; } = Array.Empty<string>();

        public TimeSpan Timestamp { get; init; }

        public TimeSpan? ThreadTimestamp { get; init; }

        public int ThreadId { get; init; }

        public int ProcessId { get; init; }

        public string? Arguments { get; init; }

        public ReservedColor Color { get; init; }

        public IReadOnlyCollection<string>? StackFrames { get; init; }

        void ISerializableTraceEvent.Serialize(StreamWriter streamWriter)
        {
            using (EventSerializationHelper.Serialize(streamWriter, this))
            {
            }
        }
    }
}