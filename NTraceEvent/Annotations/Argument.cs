namespace NTraceEvent
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    internal static class Argument
    {
        public static void NotEmpty(string? argument, [CallerArgumentExpression("argument")] string? parameterName = null)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                Argument.NotNull(argument, parameterName);

                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument {0} is empty.", parameterName));
            }
        }

        public static void NotNull([NotNull] object? argument, [CallerArgumentExpression("argument")] string? parameterName = null)
        {
#if NET6_0
            ArgumentNullException.ThrowIfNull(argument, parameterName);
#else
            if (argument is null)
            {
                throw new ArgumentNullException(parameterName);
            }
#endif
        }

        public static void InRange<T>(ref T argument, T lowerBound, T upperBound, [CallerArgumentExpression("argument")] string? parameterName = null)
            where T : struct, IComparable<T>
        {
            var lowerBoundComparisonResult = argument.CompareTo(lowerBound);
            var upperBoundComparisonResult = argument.CompareTo(upperBound);

            if (lowerBoundComparisonResult < 0 || upperBoundComparisonResult > 0)
            {
                var message = string.Format(CultureInfo.InvariantCulture, "Argument {0} must be in range [{1}, {2}].", parameterName, lowerBound, upperBound);
                throw new ArgumentOutOfRangeException(parameterName, argument, message);
            }
        }

        public static void Assert<T>(T argument, Func<T, bool> predicate, string message, [CallerArgumentExpression("argument")] string? parameterName = null)
        {
            Argument.NotNull(predicate);
            Argument.NotNull(message);

            if (!predicate(argument))
            {
                throw new ArgumentException(message, parameterName);
            }
        }
    }
}