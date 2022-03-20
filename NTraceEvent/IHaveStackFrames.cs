namespace NTraceEvent
{
    using System.Collections.Generic;

    internal interface IHaveStackFrames
    {
        public IReadOnlyCollection<string>? StackFrames { get; }
    }
}