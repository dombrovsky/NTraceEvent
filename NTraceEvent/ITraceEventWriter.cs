namespace NTraceEvent
{
    public interface ITraceEventWriter
    {
        void Write<T>(T traceEvent) where T : ISerializableTraceEvent;
    }
}