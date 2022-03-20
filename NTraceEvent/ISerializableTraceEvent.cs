namespace NTraceEvent
{
    using System.IO;

    public interface ISerializableTraceEvent
    {
        void Serialize(StreamWriter streamWriter);
    }
}