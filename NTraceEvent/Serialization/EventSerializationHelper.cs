namespace NTraceEvent
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;

    internal static class EventSerializationHelper
    {
        [SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code", Justification = "False positive")]
        public static EventSerializationScope Serialize<T>(StreamWriter streamWriter, in T traceEvent)
            where T : struct, ITraceEvent
        {
            streamWriter.Write("{ ");

            SerializeProperty(streamWriter, "ph", traceEvent.Type.EventTypeKey, isFirst: true);
            SerializeProperty(streamWriter, "pid", traceEvent.ProcessId);
            SerializeProperty(streamWriter, "tid", traceEvent.ThreadId);

            if (traceEvent is IHaveTimestamp hasTimestamp)
            {
                SerializeProperty(streamWriter, "ts", hasTimestamp.Timestamp);
                if (hasTimestamp.ThreadTimestamp.HasValue)
                {
                    SerializeProperty(streamWriter, "tts", hasTimestamp.ThreadTimestamp.Value);
                }
            }

            if (traceEvent is IHaveName hasName)
            {
                SerializeProperty(streamWriter, "name", hasName.Name);
            }

            if (traceEvent is IHaveCategory hasCategory && hasCategory.Categories.Count > 0)
            {
                SerializeProperty(streamWriter, "cat", string.Join(",", hasCategory.Categories));
            }

            if (traceEvent is IHaveColor hasColor && hasColor.Color != default)
            {
                SerializeProperty(streamWriter, "cname", hasColor.Color.Key);
            }

            if (traceEvent is IHaveArguments {Arguments: { }} hasArguments)
            {
                SerializeProperty(streamWriter, "args", hasArguments.Arguments);
            }

            if (traceEvent is IHaveStackFrames {StackFrames: {Count: > 0}} hasStackFrames)
            {
                SerializeProperty(streamWriter, "stack", hasStackFrames.StackFrames);
            }

            return new EventSerializationScope(streamWriter);
        }

        public static void SerializeProperty<T>(StreamWriter streamWriter, string key, T value, bool isFirst = false)
        {
            using (WriteValue<T>(streamWriter, key, isFirst))
            {
                switch (value)
                {
                    case IFormattable formattable:
                        streamWriter.Write(formattable.ToString(null, CultureInfo.InvariantCulture));
                        break;
                    case IEnumerable<string> stringCollection:
                        SerializeProperty(streamWriter, key, stringCollection, isFirst);
                        break;
                    default:
                        streamWriter.Write(value?.ToString());
                        break;
                }
            }
        }

        public static void SerializeProperty(StreamWriter streamWriter, string key, string value, bool isFirst = false)
        {
            using (WriteValue<string>(streamWriter, key, isFirst))
            {
                streamWriter.Write(value);
            }
        }

        public static void SerializeProperty(StreamWriter streamWriter, string key, IEnumerable<string> value, bool isFirst = false)
        {
            using (WriteValue<IEnumerable<string>>(streamWriter, key, isFirst))
            {
                streamWriter.Write('[');

                var i = 0;
                foreach (var item in value)
                {
                    if (i++ > 0)
                    {
                        streamWriter.Write(',');
                    }

                    streamWriter.Write('\"');
                    streamWriter.Write(item);
                    streamWriter.Write('\"');
                }

                streamWriter.Write(']');
            }
        }

        public static void SerializeProperty<T>(StreamWriter streamWriter, string key, IReadOnlyDictionary<string, T> value, bool isFirst = false)
            where T : struct
        {
            using (WriteValue<IReadOnlyDictionary<string, T>>(streamWriter, key, isFirst))
            {
                streamWriter.Write('{');

                var i = 0;
                foreach (var item in value)
                {
                    SerializeProperty(streamWriter, item.Key, item.Value, i++ == 0);
                }

                streamWriter.Write('}');
            }
        }

        public static void SerializeProperty(StreamWriter streamWriter, string key, IEnumerable<KeyValuePair<string, object>> value, bool isFirst = false)
        {
            using (WriteValue<IEnumerable<KeyValuePair<string, object>>>(streamWriter, key, isFirst))
            {
                streamWriter.Write('{');

                var i = 0;
                foreach (var item in value)
                {
                    SerializeProperty(streamWriter, item.Key, item.Value, i++ == 0);
                }

                streamWriter.Write('}');
            }
        }

        public static void SerializeProperty(StreamWriter streamWriter, string key, char value, bool isFirst = false)
        {
            using (WriteValue<char>(streamWriter, key, isFirst))
            {
                streamWriter.Write(value);
            }
        }

        public static void SerializeProperty(StreamWriter streamWriter, string key, TimeSpan value, bool isFirst = false)
        {
            using (WriteValue<TimeSpan>(streamWriter, key, isFirst))
            {
                streamWriter.Write(value.TotalMicroseconds());
            }
        }

        private static WriteValueScope<T> WriteValue<T>(StreamWriter streamWriter, string key, bool isFirst)
        {
            if (!isFirst)
            {
                streamWriter.Write(", ");
            }

            return new WriteValueScope<T>(streamWriter, key);
        }

        private static bool ShouldEncloseInQuotes(Type type)
        {
            return type == typeof(string) || (!IsNumericType(type) && !typeof(IEnumerable).IsAssignableFrom(type));
        }

        private static bool IsNumericType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        private readonly ref struct WriteValueScope<T>
        {
            private readonly StreamWriter _streamWriter;
            private readonly bool _shouldEncloseValueInQuotes;

            public WriteValueScope(StreamWriter streamWriter, string key)
            {
                _streamWriter = streamWriter;
                _shouldEncloseValueInQuotes = ShouldEncloseInQuotes(typeof(T));

                _streamWriter.Write('\"');
                _streamWriter.Write(key);
                _streamWriter.Write(_shouldEncloseValueInQuotes ? "\": \"" : "\": ");
            }

            public void Dispose()
            {
                if (_shouldEncloseValueInQuotes)
                {
                    _streamWriter.Write('\"');
                }
            }
        }
    }
}