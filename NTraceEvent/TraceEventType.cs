namespace NTraceEvent
{
    public readonly record struct TraceEventType(char EventTypeKey)
    {
        public static readonly TraceEventType Complete = new('X');

        public static readonly TraceEventType Instant = new('i');

        public static readonly TraceEventType DurationBegin = new('B');

        public static readonly TraceEventType DurationEnd = new('E');

        public static readonly TraceEventType Counter = new('C');
    }
}