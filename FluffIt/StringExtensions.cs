//////////////////////////////////////////////////////////////////////////////////
//
// The MIT License (MIT)
//
// Copyright (c) 2014 Furry Builder
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
//////////////////////////////////////////////////////////////////////////////////

using System;
using JetBrains.Annotations;

namespace FluffIt
{
    [PublicAPI]
    public static class StringExtensions
    {
        /// <summary>
        ///     Checks if a string is null or an empty string.
        /// </summary>
        /// <param name="source">The string to check</param>
        /// <returns>Returns true if the string is null or empty</returns>
        [PublicAPI, Pure]
        public static bool IsNullOrEmpty([CanBeNull] this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        /// <summary>
        ///     Checks if a string is null, empty or only composed of white-space characters.
        /// </summary>
        /// <param name="source">The string to check</param>
        /// <returns>Returns true if the string is null, empty or contains only white-space characters</returns>
        [PublicAPI, Pure]
        public static bool IsNullOrWhiteSpace([CanBeNull] this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        /// <summary>
        ///     Replaces each format item in a specified string with the text equivalent of a corresponding object's value.
        /// </summary>
        /// <param name="source">A composite format string</param>
        /// <param name="args">Objects to format</param>
        /// <returns>A new string with all format items replaced with their corresponding of values</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> or <paramref name="args" /> is null. </exception>
        /// <exception cref="FormatException">
        ///     <paramref name="source" /> is invalid.-or- The index of a format item is less than
        ///     zero, or greater than or equal to the length of the <paramref name="args" /> array.
        /// </exception>
        [PublicAPI, Pure, StringFormatMethod("source")]
        public static string Format([NotNull] this string source, [NotNull] params object[] args)
        {
            return string.Format(source, args);
        }

        /// <summary>
        ///     Replaces each format item in a specified string with the text equivalent of a corresponding object's value.
        /// </summary>
        /// <param name="source">A composite format string</param>
        /// <param name="provider">Provides culture specific formating information</param>
        /// <param name="args">Objects to format</param>
        /// <returns>A new string with all format items replaced with their corresponding of values</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> or <paramref name="args" /> is null. </exception>
        /// <exception cref="FormatException">
        ///     <paramref name="source" /> is invalid.-or- The index of a format item is less than
        ///     zero, or greater than or equal to the length of the <paramref name="args" /> array.
        /// </exception>
        [PublicAPI, Pure, StringFormatMethod("source")]
        public static string Format(
            [NotNull] this string source,
            [NotNull] IFormatProvider provider,
            [NotNull] params object[] args)
        {
            return string.Format(provider, source, args);
        }

        /// <summary>
        ///     Returns an empty string if the source string is null.
        /// </summary>
        /// <param name="source">The string to validate</param>
        /// <returns>Returns the original string, or an empty string if the source is null</returns>
        [PublicAPI, Pure]
        public static string Safe([CanBeNull] this string source)
        {
            return source ?? string.Empty;
        }
    }
}