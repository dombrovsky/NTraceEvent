namespace NTraceEvent
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Each complete event logically combines a pair of duration (<see cref="DurationBeginTraceEvent"/> and <see cref="DurationEndTraceEvent"/>) events.
    /// In a trace that most of the events are duration events, using complete events to replace the duration events can reduce the size of the trace to about half.
    /// <see href="https://docs.google.com/document/d/1CvAClvFfyA5R-PhYUmn5OOQtYMH4h6I0nSsKchNAySU/preview#heading=h.lpfof2aylapb"/>
    /// </summary>
    public readonly record struct CompleteTraceEvent :
        ITraceEvent, IHaveName, IHaveTimestamp, IHaveCategory, IHaveArguments, IHaveColor, ISerializableTraceEvent
    {
        public TraceEventType Type => TraceEventType.Complete;

        public string Name { get; init; } = string.Empty;

        public IReadOnlyCollection<string> Categories { get; init; } = Array.Empty<string>();

        public TimeSpan Timestamp { get; init; }

        public TimeSpan? ThreadTimestamp { get; init; }

        public int ThreadId { get; init; }

        public int ProcessId { get; init; }

        public ReservedColor Color { get; init; }

        public IEnumerable<KeyValuePair<string, object>>? Arguments { get; init; }

        /// <summary>
        /// Gets the tracing clock event duration.
        /// </summary>
        public TimeSpan Duration { get; init; }

        /// <summary>
        /// Gets the thread clock event duration.
        /// </summary>
        public TimeSpan? ThreadDuration { get; init; }

        void ISerializableTraceEvent.Serialize(StreamWriter streamWriter)
        {
            using (EventSerializationHelper.Serialize(streamWriter, this))
            {
                EventSerializationHelper.SerializeProperty(streamWriter, "dur", Duration);

                if (ThreadDuration.HasValue)
                {
                    EventSerializationHelper.SerializeProperty(streamWriter, "tdur", ThreadDuration.Value);
                }
            }
        }
    }
}