namespace NTraceEvent
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public readonly record struct CompleteTraceEvent : ITraceEvent, IHaveArguments, ISerializableTraceEvent
    {
        public TraceEventType Type => TraceEventType.Complete;

        public string Name { get; init; } = string.Empty;

        public IReadOnlyCollection<string> Categories { get; init; } = Array.Empty<string>();

        public TimeSpan Timestamp { get; init; }

        public TimeSpan? ThreadTimestamp { get; init; }

        public int ThreadId { get; init; }

        public int ProcessId { get; init; }

        public ReservedColor Color { get; init; }

        public string? Arguments { get; init; }

        public TimeSpan Duration { get; init; }

        void ISerializableTraceEvent.Serialize(StreamWriter streamWriter)
        {
            using (EventSerializationHelper.Serialize(streamWriter, this))
            {
                EventSerializationHelper.SerializeProperty(streamWriter, "dur", Duration);
            }
        }
    }
}