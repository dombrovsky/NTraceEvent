namespace NTraceEvent
{
    public readonly record struct InstantEventScope(char ScopeKey)
    {
        public static readonly InstantEventScope Global = new('g');

        public static readonly InstantEventScope Process = new('p');

        public static readonly InstantEventScope Thread = new('t');
    }
}