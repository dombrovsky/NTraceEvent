namespace NTraceEvent
{
    using System.Collections.Generic;

    internal interface IHaveArguments
    {
        /// <summary>
        /// Gets the arguments provided for the event in JSON format. 
        /// </summary>
        /// <remarks>
        /// Some of the event types have required argument fields, otherwise, you can put any information you wish in here. The arguments are displayed in Trace Viewer when you view an event in the analysis section.
        /// </remarks>
        IEnumerable<KeyValuePair<string, object>>? Arguments { get; }
    }
}