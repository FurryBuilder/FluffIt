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
using JetBrains.Annotations;

namespace FluffIt
{
    [PublicAPI]
    public static class DictionaryExtensions
    {
        /// <summary>
        ///     Safely extracts a value from a dictionary.
        /// </summary>
        /// <typeparam name="TKey">Type of the key</typeparam>
        /// <typeparam name="TValue">Type of the value</typeparam>
        /// <param name="dictionary">Dictionary holding the expected value</param>
        /// <param name="key">Key to use when retriving the value from the dictionary</param>
        /// <returns>Returns the value matching the provided key or a default value if the key cannot be found</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
        [PublicAPI]
        public static TValue GetOrDefault<TKey, TValue>(
            [NotNull] this IDictionary<TKey, TValue> dictionary,
            [NotNull] TKey key)
        {
            TValue value;

            return dictionary.TryGetValue(key, out value)
                ? value
                : default(TValue);
        }
    }
}