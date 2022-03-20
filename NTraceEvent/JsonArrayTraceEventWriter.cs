namespace NTraceEvent
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Writes trace events in JSON Array Format.
    /// <see href="https://docs.google.com/document/d/1CvAClvFfyA5R-PhYUmn5OOQtYMH4h6I0nSsKchNAySU/preview#heading=h.f2f0yd51wi15"/>
    /// </summary>
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

        /// <summary>
        /// Writes event to a stream.
        /// </summary>
        /// <typeparam name="T">The type of event.</typeparam>
        /// <param name="traceEvent">The event.</param>
        public void Write<T>(in T traceEvent)
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