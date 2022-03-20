namespace NTraceEvent
{
    using System.Collections.Generic;

    internal interface IHaveStackFrames
    {
        /// <summary>
        /// Gets the stack frame array associated with the event.
        /// </summary>
        /// <remarks>
        /// The 0th item in the array is the rootmost part of the callstack, the last item is the leafmost entry in the stack, e.g. the closest to what was running when the event was issued. You can put anything you want in each trace, but strings in hex form ("0x1234") are treated as program counter addresses and are eligible for symbolization.
        /// </remarks>
        public IReadOnlyCollection<string>? StackFrames { get; }
    }
}