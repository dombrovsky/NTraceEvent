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
    public readonly record struct DurationBeginTraceEvent
        : ITraceEvent, IHaveName, IHaveCategory, IHaveArguments, IHaveColor, IHaveTimestamp, IHaveStackFrames, ISerializableTraceEvent
    {
        public TraceEventType Type => TraceEventType.DurationBegin;

        public string Name { get; init; } = string.Empty;

        public IReadOnlyCollection<string> Categories { get; init; } = Array.Empty<string>();

        public TimeSpan Timestamp { get; init; }

        public TimeSpan? ThreadTimestamp { get; init; }

        public int ThreadId { get; init; }

        public int ProcessId { get; init; }

        public IEnumerable<KeyValuePair<string, object>>? Arguments { get; init; }

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