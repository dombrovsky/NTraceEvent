namespace NTraceEvent
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public readonly record struct CounterTraceEvent : ITraceEvent, ISerializableTraceEvent
    {
        private static readonly Dictionary<string, int> EmptyValues = new Dictionary<string, int>();

        public TraceEventType Type => TraceEventType.Counter;

        public string Name { get; init; } = string.Empty;

        public IReadOnlyCollection<string> Categories { get; init; } = Array.Empty<string>();

        public TimeSpan Timestamp { get; init; }

        public TimeSpan? ThreadTimestamp { get; init; }

        public int ThreadId { get; init; }

        public int ProcessId { get; init; }

        public ReservedColor Color { get; init; }

        public IReadOnlyDictionary<string, int> Values { get; init; } = EmptyValues;

        void ISerializableTraceEvent.Serialize(StreamWriter streamWriter)
        {
            using (EventSerializationHelper.Serialize(streamWriter, this))
            {
                EventSerializationHelper.SerializeProperty(streamWriter, "args", Values);
            }
        }
    }
}