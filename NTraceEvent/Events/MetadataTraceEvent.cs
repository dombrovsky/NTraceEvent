namespace NTraceEvent
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Metadata events are used to associate extra information with the events in the trace file. This information can be things like process names, or thread names.
    /// <see href="https://docs.google.com/document/d/1CvAClvFfyA5R-PhYUmn5OOQtYMH4h6I0nSsKchNAySU/preview#heading=h.xqopa5m0e28f"/>
    /// </summary>
    public readonly record struct MetadataTraceEvent : ITraceEvent, IHaveName, IHaveArguments, ISerializableTraceEvent
    {
        public TraceEventType Type => TraceEventType.Metadata;

        public int ThreadId { get; init; }

        public int ProcessId { get; init; }

        /// <summary>
        /// Gets the type of metadata.
        /// </summary>
        public MetadataEventType MetadataType { get; init; }

        /// <summary>
        /// Gets the value of metadata.
        /// </summary>
        /// <remarks>
        /// For the <see cref="MetadataEventType.ThreadSortIndex"/> and <see cref="MetadataEventType.ProcessSortIndex"/> items the <see cref="Value"/> argument value specifies the relative position you'd like the item to be displayed. Lower numbers are displayed higher in Trace Viewer.
        /// </remarks>
        public object Value { get; init; }

        string IHaveName.Name => MetadataType.MetadataType;

        IEnumerable<KeyValuePair<string, object>>? IHaveArguments.Arguments
        {
            get
            {
                yield return new KeyValuePair<string, object>(MetadataType.ValueKey, Value);
            }
        }

        void ISerializableTraceEvent.Serialize(StreamWriter streamWriter)
        {
            using (EventSerializationHelper.Serialize(streamWriter, this))
            {
            }
        }
    }
}