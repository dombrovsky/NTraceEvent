namespace NTraceEvent
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The instant events correspond to something that happens but has no duration associated with it. For example, vblank events are considered instant events.
    /// <see href="https://docs.google.com/document/d/1CvAClvFfyA5R-PhYUmn5OOQtYMH4h6I0nSsKchNAySU/preview#heading=h.lenwiilchoxp"/>
    /// </summary>
    public readonly record struct InstantTraceEvent : ITraceEvent, IHaveArguments, IHaveName, IHaveStackFrames, ISerializableTraceEvent
    {
        public TraceEventType Type => TraceEventType.Instant;

        /// <summary>
        /// Gets the scope of the event.
        /// </summary>
        /// <remarks>
        /// The scope of the event designates how tall to draw the instant event in Trace Viewer. A thread scoped event will draw the height of a single thread. A process scoped event will draw through all threads of a given process. A global scoped event will draw a time from the top to the bottom of the timeline.
        /// </remarks>
        public InstantEventScope Scope { get; init; } = InstantEventScope.Thread;

        public string Name { get; init; } = string.Empty;

        public IReadOnlyCollection<string> Categories { get; init; } = Array.Empty<string>();

        public TimeSpan Timestamp { get; init; }

        public TimeSpan? ThreadTimestamp { get; init; }

        public int ThreadId { get; init; }

        public ReservedColor Color { get; init; }

        public int ProcessId { get; init; }

        public string? Arguments { get; init; }

        /// <remarks>
        /// Process-scoped and global-scoped events do not support stack traces at this time.
        /// </remarks>
        public IReadOnlyCollection<string>? StackFrames { get; init; }

        void ISerializableTraceEvent.Serialize(StreamWriter streamWriter)
        {
            using (EventSerializationHelper.Serialize(streamWriter, this))
            {
                if (Scope != default)
                {
                    EventSerializationHelper.SerializeProperty(streamWriter, "s", Scope.ScopeKey);
                }
            }
        }
    }
}