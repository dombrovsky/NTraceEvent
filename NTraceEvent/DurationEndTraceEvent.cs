namespace NTraceEvent
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Duration events provide a way to mark a duration of work on a given thread.
    /// The <see cref="DurationBeginTraceEvent"/> event must come before the corresponding <see cref="DurationEndTraceEvent"/> event.
    /// You can nest the <see cref="DurationBeginTraceEvent"/> and <see cref="DurationEndTraceEvent"/> events. This allows you to capture function calling behaviour on a thread.
    /// The timestamps for the duration events must be in increasing order for a given thread. Timestamps in different threads do not have to be in increasing order, just the timestamps within a given thread.
    /// <see href="https://docs.google.com/document/d/1CvAClvFfyA5R-PhYUmn5OOQtYMH4h6I0nSsKchNAySU/preview#heading=h.nso4gcezn7n1"/>
    /// </summary>
    /// <remarks>
    /// The only required fields for the <see cref="DurationEndTraceEvent"/> events are the <see cref="ProcessId"/>, <see cref="ThreadId"/>, <see cref="Type"/> and <see cref="Timestamp"/> fields, all others are optional.
    /// If you provide <see cref="Arguments"/> to both the <see cref="DurationBeginTraceEvent"/> and <see cref="DurationEndTraceEvent"/> events then the arguments will be merged. If there is a duplicate argument value provided the <see cref="DurationEndTraceEvent"/> event argument will be taken and the <see cref="DurationBeginTraceEvent"/> event argument will be discarded.
    /// </remarks>
    public readonly record struct DurationEndTraceEvent : ITraceEvent, IHaveArguments, ISerializableTraceEvent
    {
        public TraceEventType Type => TraceEventType.DurationEnd;

        public TimeSpan Timestamp { get; init; }

        public TimeSpan? ThreadTimestamp { get; init; }

        public int ThreadId { get; init; }

        public int ProcessId { get; init; }

        public ReservedColor Color { get; init; }

        public string? Arguments { get; init; }

        void ISerializableTraceEvent.Serialize(StreamWriter streamWriter)
        {
            using (EventSerializationHelper.Serialize(streamWriter, this))
            {
            }
        }
    }
}