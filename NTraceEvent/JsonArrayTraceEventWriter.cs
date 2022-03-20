namespace NTraceEvent
{
    using System;
    using System.IO;
    using System.Text;

    public sealed class JsonArrayTraceEventWriter : ITraceEventWriter, IDisposable
    {
        private readonly StreamWriter _streamWriter;

        private bool _isNotFirstEvent;
        private bool _isDisposed;

        public JsonArrayTraceEventWriter(Stream stream)
        {
            Argument.NotNull(stream);

            _streamWriter = new StreamWriter(stream, Encoding.UTF8, 4096, true);

            _streamWriter.Write("[ ");
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _streamWriter.Write(" ]");
                _streamWriter.Dispose();
                _isDisposed = true;
            }
        }

        public void Write<T>(T traceEvent)
            where T : ISerializableTraceEvent
        {
            Argument.NotNull(traceEvent);

            if (_isNotFirstEvent)
            {
                _streamWriter.Write(", ");
            }
            else
            {
                _isNotFirstEvent = true;
            }

            traceEvent.Serialize(_streamWriter);
            _streamWriter.Flush();
        }
    }
}