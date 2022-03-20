namespace NTraceEvent
{
    using System.Collections.Generic;

    internal interface IHaveCategory
    {
        /// <summary>
        /// Gets the list of categories for the event.
        /// </summary>
        /// <remarks>The categories can be used to hide events in the Trace Viewer UI.</remarks>
        IReadOnlyCollection<string> Categories { get; }
    }
}