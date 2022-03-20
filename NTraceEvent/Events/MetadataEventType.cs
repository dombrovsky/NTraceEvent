namespace NTraceEvent
{
    public readonly record struct MetadataEventType(string MetadataType, string ValueKey)
    {
        /// <summary>
        /// Sets the display name for the provided <see cref="ITraceEvent.ProcessId"/>.
        /// </summary>
        public static readonly MetadataEventType ProcessName = new("process_name", "name");

        /// <summary>
        /// Sets the extra process labels for the provided <see cref="ITraceEvent.ProcessId"/>.
        /// </summary>
        public static readonly MetadataEventType ProcessLabels = new("process_labels", "labels");

        /// <summary>
        /// Sets the process sort order position.
        /// </summary>
        public static readonly MetadataEventType ProcessSortIndex = new("process_sort_index", "sort_index");

        /// <summary>
        /// Sets the name for the given <see cref="ITraceEvent.ThreadId"/>.
        /// </summary>
        public static readonly MetadataEventType ThreadName = new("thread_name", "name");

        /// <summary>
        ///  Sets the thread sort order position.
        /// </summary>
        public static readonly MetadataEventType ThreadSortIndex = new("thread_sort_index", "sort_index");
    }
}