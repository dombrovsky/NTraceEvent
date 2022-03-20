namespace NTraceEvent
{
    internal interface IHaveColor
    {
        /// <summary>
        /// Gets a fixed color name to associate with the event. If provided, cname must be one of the names listed in <see cref="ReservedColor"/>.
        /// </summary>
        ReservedColor Color { get; }
    }
}