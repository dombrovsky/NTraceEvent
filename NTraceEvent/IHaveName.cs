namespace NTraceEvent
{
    using System.Collections.Generic;

    internal interface IHaveName
    {
        /// <summary>
        /// Gets the name of the event;
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the list of categories for the event.
        /// </summary>
        /// <remarks>The categories can be used to hide events in the Trace Viewer UI.</remarks>
        IReadOnlyCollection<string> Categories { get; }
    }
}