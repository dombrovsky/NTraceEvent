namespace NTraceEvent
{
    public interface ITraceEventWriter
    {
        void Write<T>(in T traceEvent) where T : ISerializableTraceEvent;
    }
}