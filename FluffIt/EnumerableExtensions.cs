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
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace FluffIt
{
    [PublicAPI]
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Iterates over an enumerable source and execute an action for each elements.
        /// </summary>
        /// <typeparam name="TSource">Type of the element contained in the enumerable source</typeparam>
        /// <param name="source">The enumerable source to iterate over</param>
        /// <param name="action">The action to execute for each element in the source</param>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        [PublicAPI]
        public static void ForEach<TSource>(
            [NotNull, InstantHandle] this IEnumerable<TSource> source,
            [NotNull, InstantHandle] Action<TSource> action)
        {
            foreach (var item in source)
            {
                action.Invoke(item);
            }
        }

        /// <summary>
        ///     Checks wether an enumerable contains no value.
        /// </summary>
        /// <typeparam name="TSource">Type of the elemnt contained in the enumerable source</typeparam>
        /// <param name="source">The enumerable to validate</param>
        /// <returns>Returns true if the source contains no elements</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is null.</exception>
        [PublicAPI]
        public static bool None<TSource>([NotNull, InstantHandle] this IEnumerable<TSource> source)
        {
            return !source.Any();
        }

        /// <summary>
        ///     Checks wether an enumerable contains no value matching a predicate.
        /// </summary>
        /// <typeparam name="TSource">Type of the elemnt contained in the enumerable source</typeparam>
        /// <param name="source">The enumerable to validate</param>
        /// <param name="predicate">The function to use as predicate</param>
        /// <returns>Returns true if the source contains no elements that matches the predicate</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
        [PublicAPI]
        public static bool None<TSource>(
            [NotNull, InstantHandle] this IEnumerable<TSource> source,
            [NotNull, InstantHandle] Func<TSource, bool> predicate)
        {
            return !source.Any(predicate);
        }

        /// <summary>
        ///     Returns the first element of a sequence that satisfies a comparer using a provided value.
        /// </summary>
        /// <typeparam name="TResult">Type of the element in the sequence</typeparam>
        /// <param name="source">Source enumerable to look for elements</param>
        /// <param name="comparer">Comparer object to use when matching the provided value to the various elements</param>
        /// <param name="value">Value to look for in the elements</param>
        /// <returns>The first element that matches the value using the comparer</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> or <paramref name="comparer" /> is null.</exception>
        /// <exception cref="InvalidOperationException">
        ///     No element satisfies the condition in <paramref name="comparer" />.-or-The
        ///     source sequence is empty.
        /// </exception>
        [PublicAPI]
        public static TResult First<TResult>(
            [NotNull, InstantHandle] this IEnumerable<TResult> source,
            [NotNull] IEqualityComparer<TResult> comparer,
            [CanBeNull] TResult value)
        {
            return source.First(t => comparer.Equals(t, value));
        }

        /// <summary>
        ///     Returns the first element of a sequence that satisfies a comparer using a provided value, or a default value if no
        ///     such element is found.
        /// </summary>
        /// <typeparam name="TResult">Type of the element in the sequence</typeparam>
        /// <param name="source">Source enumerable to look for elements</param>
        /// <param name="comparer">Comparer object to use when matching the provided value to the various elements</param>
        /// <param name="value">Value to look for in the elements</param>
        /// <returns>The first element that matches the value using the comparer or a default value if no element matches</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> or <paramref name="comparer" /> is null.</exception>
        [PublicAPI]
        public static TResult FirstOrDefault<TResult>(
            [NotNull, InstantHandle] this IEnumerable<TResult> source,
            [NotNull] IEqualityComparer<TResult> comparer,
            [CanBeNull] TResult value)
        {
            return source.FirstOrDefault(t => comparer.Equals(t, value));
        }

        /// <summary>
        ///     Returns the first element of a sequence that satisfies a comparer using a provided value, or a default value if no
        ///     such element is found.
        /// </summary>
        /// <typeparam name="TResult">Type of the element in the sequence</typeparam>
        /// <param name="source">Source enumerable to look for elements</param>
        /// <param name="comparer">Comparer object to use when matching the provided value to the various elements</param>
        /// <param name="value">Value to look for in the elements</param>
        /// <param name="defaultValueFactory">Function used to provide a default value if no matching element have been found</param>
        /// <returns>The first element that matches the value using the comparer or a default value if no element matches</returns>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> or <paramref name="comparer" /> is null.</exception>
        [PublicAPI]
        public static TResult FirstOrDefault<TResult>(
            [NotNull, InstantHandle] this IEnumerable<TResult> source,
            [NotNull] IEqualityComparer<TResult> comparer,
            [CanBeNull] TResult value,
            [NotNull, InstantHandle] Func<TResult> defaultValueFactory)
        {
            foreach (var result in source.Where(s => comparer.Equals(s, value)))
            {
                return result;
            }

            return defaultValueFactory.Invoke();
        }

        /// <summary>
        ///     Returns the first element of a key value pair sequence that matches the provided key.
        /// </summary>
        /// <typeparam name="TKey">Type of the key inside the sequence</typeparam>
        /// <typeparam name="TValue">Type of the value inside the sequence</typeparam>
        /// <param name="source">Source enumerable to took for elements</param>
        /// <param name="needle">The key to find inside the source enumerable</param>
        /// <returns>The value matching the provided key, or a default value if no element matches</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is null.</exception>
        [PublicAPI]
        public static TValue FirstOrDefault<TKey, TValue>(
            [NotNull, InstantHandle] this IEnumerable<KeyValuePair<TKey, TValue>> source,
            [CanBeNull] TKey needle)
        {
            return source
                .FirstOrDefault(kv => Equals(kv.Key, needle))
                .SelectOrDefault(kv => kv.Value);
        }

        /// <summary>
        ///     Returns the first element of a key value pair sequence that matches the provided key.
        /// </summary>
        /// <typeparam name="TKey">Type of the key inside the sequence</typeparam>
        /// <typeparam name="TValue">Type of the value inside the sequence</typeparam>
        /// <param name="source">Source enumerable to took for elements</param>
        /// <param name="comparer">The comparer to use when looking for a matching key</param>
        /// <param name="needle">The key to find inside the source enumerable</param>
        /// <returns>The value matching the provided key, or a default value if no element matches</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> or <paramref name="comparer" /> is null.</exception>
        [PublicAPI]
        public static TValue FirstOrDefault<TKey, TValue>(
            [NotNull, InstantHandle] this IEnumerable<KeyValuePair<TKey, TValue>> source,
            [NotNull] IEqualityComparer<TKey> comparer,
            [CanBeNull] TKey needle)
        {
            return source
                .FirstOrDefault(kv => comparer.Equals(kv.Key, needle))
                .SelectOrDefault(kv => kv.Value);
        }

        /// <summary>
        ///     Iterates over a sequence of element and execute an action for each of them.
        /// </summary>
        /// <typeparam name="TSource">Type of the elements in the enumerable</typeparam>
        /// <param name="source">Sequence to iterate over</param>
        /// <param name="action">Action to execute for each element</param>
        /// <returns>Returns the original sequence</returns>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        [PublicAPI]
        public static IEnumerable<TSource> Do<TSource>(
            [NotNull, InstantHandle] this IEnumerable<TSource> source,
            [NotNull, InstantHandle] Action<TSource> action)
        {
            foreach (var v in source)
            {
                action.Invoke(v);

                yield return v;
            }
        }

        /// <summary>
        ///     Inserts a value at the begining of a sequence.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the sequence</typeparam>
        /// <param name="source">Sequence to update</param>
        /// <param name="value">Value to insert</param>
        /// <returns>Returns a new sequence containing the inserted value</returns>
        [PublicAPI]
        public static IEnumerable<TSource> Prepend<TSource>(
            [NotNull, InstantHandle] this IEnumerable<TSource> source,
            [CanBeNull] TSource value)
        {
            yield return value;

            foreach (var v in source)
            {
                yield return v;
            }
        }

        /// <summary>
        ///     Inserts a value at the end of a sequence.
        /// </summary>
        /// <typeparam name="TSource">Type of the elements in the sequence</typeparam>
        /// <param name="source">Sequence to update</param>
        /// <param name="value">Value to insert</param>
        /// <returns>Returns a new sequence containing the inserted value</returns>
        [PublicAPI]
        public static IEnumerable<TSource> Append<TSource>(
            [NotNull, InstantHandle] this IEnumerable<TSource> source,
            [CanBeNull] TSource value)
        {
            foreach (var v in source)
            {
                yield return v;
            }

            yield return value;
        }

        /// <summary>
        ///     Returns an empty sequence of element if the original sequence is null.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        [PublicAPI, Pure]
        public static IEnumerable<TSource> Safe<TSource>([CanBeNull] this IEnumerable<TSource> source)
        {
            return source ?? Enumerable.Empty<TSource>();
        }

        /// <summary>
        ///     Ensures that a sequence contains at least a certain amount of elements.
        /// </summary>
        /// <typeparam name="TSource">Type of the elements in the sequence</typeparam>
        /// <param name="source">Sequence to validate</param>
        /// <param name="count">Amount of required element in the sequence</param>
        /// <returns>Returns true if the sequence as at least count elements, false otherwise</returns>
        [PublicAPI]
        public static bool Require<TSource>([NotNull, InstantHandle] this IEnumerable<TSource> source, int count)
        {
            var i = 0;

            foreach (var v in source)
            {
                ++i;

                if (i >= count)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Ensures that a sequence contains at least a certain amount of elements that matches a specified predicate.
        /// </summary>
        /// <typeparam name="TSource">Type of the elements in the sequence</typeparam>
        /// <param name="source">Sequence to validate</param>
        /// <param name="count">Amount of required element in the sequence</param>
        /// <param name="predicate">Function that validates if an element should be included in the count</param>
        /// <returns>Returns true if the sequence as at least count elements, false otherwise</returns>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        [PublicAPI]
        public static bool Require<TSource>(
            [NotNull, InstantHandle] this IEnumerable<TSource> source,
            int count,
            [NotNull, InstantHandle] Func<TSource, bool> predicate)
        {
            var i = 0;

            foreach (var v in source)
            {
                if (predicate.Invoke(v))
                {
                    ++i;
                }

                if (i >= count)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Maps a sequence of actions over to a sequence of elements and execute each actions over their corresponding
        ///     element.
        ///     Stops executing when all actions have been exhausted.
        /// </summary>
        /// <typeparam name="TSource">Type of the element in the sequence</typeparam>
        /// <param name="source">Sequence of element to use as data in the sequence of actions</param>
        /// <param name="actions">Sequence of actions to execute over each element in the source sequence</param>
        [PublicAPI]
        public static void Distribute<TSource>(
            [NotNull, InstantHandle] this IEnumerable<TSource> source,
            [NotNull, InstantHandle] params Action<TSource>[] actions)
        {
            var actionEnumerator = actions.GetEnumerator();

            foreach (var item in source)
            {
                if (!actionEnumerator.MoveNext())
                {
                    return;
                }

                actionEnumerator.Current.As<Action<TSource>>().Invoke(item);
            }
        }

        /// <summary>
        ///     Maps a sequence of actions over to a sequence of elements and execute each actions over their corresponding
        ///     element.
        ///     Continues executing using an overflow action when there is more elements provided than actions.
        /// </summary>
        /// <typeparam name="TSource">Type of the element in the sequence</typeparam>
        /// <param name="source">Sequence of element to use as data in the sequence of actions</param>
        /// <param name="overflow">Action to use when all actions have been exhausted</param>
        /// <param name="actions">Sequence of actions to execute over each element in the source sequence</param>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        [PublicAPI]
        public static void DistributeWithOverflow<TSource>(
            [NotNull, InstantHandle] this IEnumerable<TSource> source,
            [NotNull, InstantHandle] Action<TSource> overflow,
            [NotNull, InstantHandle] params Action<TSource>[] actions)
        {
            var actionEnumerator = actions.GetEnumerator();

            foreach (var item in source)
            {
                if (!actionEnumerator.MoveNext())
                {
                    overflow.Invoke(item);

                    continue;
                }

                actionEnumerator.Current.As<Action<TSource>>().Invoke(item);
            }
        }
    }
}