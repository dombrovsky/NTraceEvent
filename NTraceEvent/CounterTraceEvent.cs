namespace NTraceEvent
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The counter events can track a value or multiple values as they change over time. Each counter can be provided with multiple series of data to display. When multiple series are provided they will be displayed as a stacked area chart in Trace Viewer.
    /// <see href="https://docs.google.com/document/d/1CvAClvFfyA5R-PhYUmn5OOQtYMH4h6I0nSsKchNAySU/preview#heading=h.msg3086636uq"/>
    /// </summary>
    public readonly record struct CounterTraceEvent : ITraceEvent, IHaveName, ISerializableTraceEvent
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