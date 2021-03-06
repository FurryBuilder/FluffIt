﻿//////////////////////////////////////////////////////////////////////////////////
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
using JetBrains.Annotations;

namespace FluffIt
{
    [PublicAPI]
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Checks if the value is null or a default value.
        /// </summary>
        /// <typeparam name="TSource">Type of the value to check</typeparam>
        /// <param name="source">Value to check</param>
        /// <param name="valueTypeComparer">Comparer to use when checking for default value. Optional</param>
        /// <returns>True if the value is null or uses its default value.</returns>
        [PublicAPI]
        public static bool IsNullOrDefault<TSource>(
            [CanBeNull] this TSource source,
            [CanBeNull] IEqualityComparer<TSource> valueTypeComparer = null)
        {
            return
                ReferenceEquals(source, null) ||
                valueTypeComparer == null
                    ? Equals(source, default(TSource))
                    : valueTypeComparer.Equals(source, default(TSource));
        }

        /// <summary>
        ///     Override null and default values with a new value.
        /// </summary>
        /// <typeparam name="TSource">Type of the value to override</typeparam>
        /// <param name="source">Value to override</param>
        /// <param name="defaultValueFactory">Value factory to use when the source needs to be overriden.</param>
        /// <param name="valueTypeComparer">Comparer to use when checking for default value. Optional</param>
        /// <returns>Returns a new value if the source is null or uses a default value, else returns the untouched source.</returns>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        [PublicAPI]
        public static TSource Default<TSource>(
            [CanBeNull] this TSource source,
            [NotNull, InstantHandle] Func<TSource> defaultValueFactory,
            [CanBeNull] IEqualityComparer<TSource> valueTypeComparer = null)
        {
            return source.IsNullOrDefault(valueTypeComparer)
                ? defaultValueFactory.Invoke()
                : source;
        }

        /// <summary>
        ///     Overrides null and default values with a new value.
        /// </summary>
        /// <typeparam name="TSource">Type of the value to override</typeparam>
        /// <param name="source">Value to override</param>
        /// <param name="defaultValue">Value to use when the source needs to be overriden.</param>
        /// <param name="valueTypeComparer">Comparer to use when checking for default value. Optional</param>
        /// <returns>Returns a new value if the source is null or uses a default value, else returns the untouched source.</returns>
        [PublicAPI]
        public static TSource Default<TSource>(
            [CanBeNull] this TSource source,
            [CanBeNull] TSource defaultValue,
            [CanBeNull] IEqualityComparer<TSource> valueTypeComparer = null)
        {
            return source.IsNullOrDefault(valueTypeComparer)
                ? defaultValue
                : source;
        }

        /// <summary>
        ///     Projects the source element to a new form if the source is not null or of default value.
        /// </summary>
        /// <typeparam name="TSource">Type of the value to project</typeparam>
        /// <typeparam name="TResult">Type of the projected result</typeparam>
        /// <param name="source">Value to project</param>
        /// <param name="selector">Transformation function to invoke on source</param>
        /// <param name="valueTypeComparer">Comparer to use when checking for default value. Optional</param>
        /// <returns>The projected value resulting from the transformation function.</returns>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        [PublicAPI]
        public static TResult SelectOrDefault<TSource, TResult>(
            [CanBeNull] this TSource source,
            [NotNull, InstantHandle] Func<TSource, TResult> selector,
            [CanBeNull] IEqualityComparer<TSource> valueTypeComparer = null)
        {
            return source.IsNullOrDefault(valueTypeComparer)
                ? default(TResult)
                : selector.Invoke(source);
        }

        /// <summary>
        ///     Projects the source element to a new form if the source is not null or of default value.
        /// </summary>
        /// <typeparam name="TSource">Type of the value to project</typeparam>
        /// <typeparam name="TResult">Type of the projected result</typeparam>
        /// <param name="source">Value to project</param>
        /// <param name="selector">Transformation function to invoke on source</param>
        /// <param name="defaultValueFactory">Value factory to use when the source is null or default</param>
        /// <param name="valueTypeComparer">Comparer to use when checking for default value. Optional</param>
        /// <returns>The projected value resulting from the transformation function.</returns>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        [PublicAPI]
        public static TResult SelectOrDefault<TSource, TResult>(
            [CanBeNull] this TSource source,
            [NotNull, InstantHandle] Func<TSource, TResult> selector,
            [NotNull, InstantHandle] Func<TResult> defaultValueFactory,
            [CanBeNull] IEqualityComparer<TSource> valueTypeComparer = null
            )
        {
            return source.IsNullOrDefault(valueTypeComparer)
                ? defaultValueFactory.Invoke()
                : selector.Invoke(source);
        }

        /// <summary>
        ///     Projects the source element to a new form if the source is not null or of default value.
        /// </summary>
        /// <typeparam name="TSource">Type of the value to project</typeparam>
        /// <typeparam name="TResult">Type of the projected result</typeparam>
        /// <param name="source">Value to project</param>
        /// <param name="selector">Transformation function to invoke on source</param>
        /// <param name="defaultValue">Value to use when the source is null or default</param>
        /// <param name="valueTypeComparer">Comparer to use when checking for default value. Optional</param>
        /// <returns>The projected value resulting from the transformation function.</returns>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        [PublicAPI]
        public static TResult SelectOrDefault<TSource, TResult>(
            [CanBeNull] this TSource source,
            [NotNull, InstantHandle] Func<TSource, TResult> selector,
            [CanBeNull] TResult defaultValue,
            [CanBeNull] IEqualityComparer<TSource> valueTypeComparer = null
            )
        {
            return source.IsNullOrDefault(valueTypeComparer)
                ? defaultValue
                : selector.Invoke(source);
        }

        /// <summary>
        ///     Projects the source key value pair to a new form if the key is not null or of default value.
        /// </summary>
        /// <typeparam name="TKey">Type of the key to project</typeparam>
        /// <typeparam name="TValue">Type of the value to project</typeparam>
        /// <typeparam name="TResult">Type of the projected result</typeparam>
        /// <param name="source">Value to project</param>
        /// <param name="selector">Transformation function to invoke on source</param>
        /// <param name="valueTypeComparer">Comparer to use when checking for default value. Optional</param>
        /// <returns>The projected value resulting from the transformation function.</returns>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        [PublicAPI]
        public static TResult SelectOrDefaultKey<TKey, TValue, TResult>(
            this KeyValuePair<TKey, TValue> source,
            [NotNull, InstantHandle] Func<KeyValuePair<TKey, TValue>, TResult> selector,
            [CanBeNull] IEqualityComparer<TKey> valueTypeComparer = null
            )
        {
            return source.Key.IsNullOrDefault(valueTypeComparer)
                ? default(TResult)
                : selector.Invoke(source);
        }

        /// <summary>
        ///     Projects the source key value pair to a new form if the key is not null or of default value.
        /// </summary>
        /// <typeparam name="TKey">Type of the key to project</typeparam>
        /// <typeparam name="TValue">Type of the value to project</typeparam>
        /// <typeparam name="TResult">Type of the projected result</typeparam>
        /// <param name="source">Value to project</param>
        /// <param name="selector">Transformation function to invoke on source</param>
        /// <param name="defaultValueFactory">Value factory to use when the source is null or default</param>
        /// <param name="valueTypeComparer">Comparer to use when checking for default value. Optional</param>
        /// <returns>The projected value resulting from the transformation function.</returns>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        [PublicAPI]
        public static TResult SelectOrDefaultKey<TKey, TValue, TResult>(
            this KeyValuePair<TKey, TValue> source,
            [NotNull, InstantHandle] Func<KeyValuePair<TKey, TValue>, TResult> selector,
            [NotNull, InstantHandle] Func<TResult> defaultValueFactory,
            [CanBeNull] IEqualityComparer<TKey> valueTypeComparer = null
            )
        {
            return source.Key.IsNullOrDefault(valueTypeComparer)
                ? defaultValueFactory.Invoke()
                : selector.Invoke(source);
        }

        /// <summary>
        ///     Projects the source key value pair to a new form if the key is not null or of default value.
        /// </summary>
        /// <typeparam name="TKey">Type of the key to project</typeparam>
        /// <typeparam name="TValue">Type of the value to project</typeparam>
        /// <typeparam name="TResult">Type of the projected result</typeparam>
        /// <param name="source">Value to project</param>
        /// <param name="selector">Transformation function to invoke on source</param>
        /// <param name="defaultValue">Value to use when the source is null or default</param>
        /// <param name="valueTypeComparer">Comparer to use when checking for default value. Optional</param>
        /// <returns>The projected value resulting from the transformation function.</returns>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        [PublicAPI]
        public static TResult SelectOrDefaultKey<TKey, TValue, TResult>(
            this KeyValuePair<TKey, TValue> source,
            [NotNull, InstantHandle] Func<KeyValuePair<TKey, TValue>, TResult> selector,
            [CanBeNull] TResult defaultValue,
            [CanBeNull] IEqualityComparer<TKey> valueTypeComparer = null
            )
        {
            return source.Key.IsNullOrDefault(valueTypeComparer)
                ? defaultValue
                : selector.Invoke(source);
        }

        /// <summary>
        ///     Invokes a method on a source only when the source is not null or uses a default value.
        /// </summary>
        /// <typeparam name="TSource">Type of the value to check</typeparam>
        /// <param name="source">Value to check for null or default value</param>
        /// <param name="maybe">Function to invoke on the source</param>
        /// <param name="valueTypeComparer">Comparer to use when checking for default value. Optional</param>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        [PublicAPI]
        public static void Maybe<TSource>(
            [CanBeNull] this TSource source,
            [NotNull, InstantHandle] Action<TSource> maybe,
            [CanBeNull] IEqualityComparer<TSource> valueTypeComparer = null)
        {
            if (!source.IsNullOrDefault(valueTypeComparer))
            {
                maybe.Invoke(source);
            }
        }

        /// <summary>
        ///     Casts an input value to a target type and returns the new value.
        /// </summary>
        /// <typeparam name="TResult">The type to cast to</typeparam>
        /// <param name="source">The source value to cast</param>
        /// <returns>The value casted into the specified type or null if the cast fails</returns>
        [PublicAPI, Pure]
        public static TResult As<TResult>([CanBeNull] this object source)
            where TResult : class
        {
            return source as TResult;
        }

        /// <summary>
        ///     Casts an input value to a target type and call a specified action on success.
        ///     Mostly used to create the equivalent of a switch on the type of a value.
        /// </summary>
        /// <typeparam name="TSource">The type to cast from</typeparam>
        /// <typeparam name="TResult">The type to cast to</typeparam>
        /// <param name="source">The source value to cast</param>
        /// <param name="maybe">The action to execute if the cast is successfull</param>
        /// <returns>The original, unconverted value</returns>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        [PublicAPI]
        public static TSource MaybeAs<TSource, TResult>(
            [CanBeNull] this TSource source,
            [NotNull, InstantHandle] Action<TResult> maybe)
            where TResult : class
        {
            var result = source as TResult;

            result.Maybe(maybe);

            return source;
        }

        /// <summary>
        ///     Ensures that an action is executed in a thread-safe context.
        /// </summary>
        /// <typeparam name="TLocker">Type of the value to use as locker</typeparam>
        /// <param name="locker">The value to use as locker</param>
        /// <param name="isLocked">Check use to validate if the locker is already locked</param>
        /// <param name="lockedAction">Action to execute behind the lock</param>
        /// <exception cref="Exception">A delegate callback throws an exception. </exception>
        [PublicAPI]
        public static void DoubleCheckedLocked<TLocker>(
            [NotNull] this TLocker locker,
            [NotNull, InstantHandle] Func<TLocker, bool> isLocked,
            [NotNull, InstantHandle] Action<TLocker> lockedAction)
            where TLocker : class
        {
            if (isLocked.Invoke(locker))
            {
                lock (locker)
                {
                    if (isLocked.Invoke(locker))
                    {
                        lockedAction.Invoke(locker);
                    }
                }
            }
        }
    }
}