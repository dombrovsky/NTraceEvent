namespace NTraceEvent.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using NUnit.Framework;
    using System.Reflection;

    [TestFixture]
    public class JsonArrayTraceEventWriterFixture
    {
        [Test]
        public void TestWrite()
        {
            var outputFileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "export.json");

            using var stream = File.Create(outputFileName);
            using var writer = new JsonArrayTraceEventWriter(stream);

            writer.Write(new CompleteTraceEvent
            {
                Timestamp = TimeSpan.FromMinutes(1),
                Categories = new[] { "cat1", "cat2" },
                ProcessId = 1,
                ThreadId = 2,
                ThreadTimestamp = TimeSpan.FromMinutes(2),
                Duration = TimeSpan.FromSeconds(5),
                Name = "complete1",
            });

            writer.Write(new CounterTraceEvent
            {
                ProcessId = 1,
                Timestamp = TimeSpan.FromMinutes(1.5),
                Values = new Dictionary<string, int> { { "foo", 10 }, { "bar", 5 } },
            });

            writer.Write(new CounterTraceEvent
            {
                ProcessId = 1,
                Timestamp = TimeSpan.FromMinutes(1.8),
                Values = new Dictionary<string, int> { { "foo", 4 }, { "bar", 8 } },
            });

            writer.Write(new CounterTraceEvent
            {
                ProcessId = 1,
                Timestamp = TimeSpan.FromMinutes(3),
                Values = new Dictionary<string, int> { { "foo", 1 }, { "bar", 1 } },
            });

            writer.Write(new DurationBeginTraceEvent
            {
                Timestamp = TimeSpan.FromMinutes(1),
                Categories = new[] { "cat3" },
                ProcessId = 1,
                ThreadId = 3,
            });

            writer.Write(new DurationEndTraceEvent
            {
                Timestamp = TimeSpan.FromMinutes(3),
                ProcessId = 1,
                ThreadId = 3,
            });

            writer.Write(new InstantTraceEvent
            {
                Timestamp = TimeSpan.FromMinutes(1.3),
                Name = "instant1",
                ProcessId = 1,
                Scope = InstantEventScope.Process,
            });

            writer.Write(new MetadataTraceEvent
            {
                ProcessId = 1,
                ThreadId = 3,
                MetadataType = MetadataEventType.ProcessName,
                Value = "notepad",
            });
        }
    }
}