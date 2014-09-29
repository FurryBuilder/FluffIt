using System.Collections.Generic;

namespace Fluff.Extensions
{
	public static class DictionaryExtensions
	{
		public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
		{
			TValue value;

			return dictionary.TryGetValue(key, out value)
				? value
				: default(TValue);
		}
	}
}