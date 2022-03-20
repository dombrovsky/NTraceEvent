namespace NTraceEvent
{
    using System.IO;

    internal readonly ref struct EventSerializationScope
    {
        private readonly StreamWriter _streamWriter;

        public EventSerializationScope(StreamWriter streamWriter)
        {
            _streamWriter = streamWriter;
        }

        public void Dispose()
        {
            _streamWriter.Write(" }");
        }
    }
}
