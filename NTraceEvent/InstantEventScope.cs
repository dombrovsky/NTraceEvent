namespace NTraceEvent
{
    public readonly record struct InstantEventScope(char ScopeKey)
    {
        /// <summary>
        /// Will draw the height of a single thread.
        /// </summary>
        public static readonly InstantEventScope Thread = new('t');

        /// <summary>
        /// Will draw through all threads of a given process.
        /// </summary>
        public static readonly InstantEventScope Process = new('p');

        /// <summary>
        /// Will draw a time from the top to the bottom of the timeline.
        /// </summary>
        public static readonly InstantEventScope Global = new('g');
    }
}