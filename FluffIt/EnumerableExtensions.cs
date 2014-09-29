using System;
using System.Collections.Generic;
using System.Linq;

namespace Fluff.Extensions
{
	public static class EnumerableExtensions
	{
		public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
		{
			foreach (var item in source)
			{
				action.Invoke(item);
			}
		}

		public static bool None<TSource>(this IEnumerable<TSource> source)
		{
			return !source.Any();
		}

		public static bool None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return !source.Any(predicate.Invoke);
		}

		public static TResult First<TResult>(this IEnumerable<TResult> source, IEqualityComparer<TResult> comparer, TResult value)
		{
			return source.First(t => comparer.Equals(t, value));
		}

		public static TResult FirstOrDefault<TResult>(this IEnumerable<TResult> source, IEqualityComparer<TResult> comparer, TResult value)
		{
			return source.FirstOrDefault(t => comparer.Equals(t, value));
		}

		public static TResult FirstOrDefault<TResult>(this IEnumerable<TResult> source, IEqualityComparer<TResult> comparer, [CanBeNull] TResult value, [NotNull] Func<TResult> defaultValueFactory)
		{
			foreach (var result in source.Where(s => comparer.Equals(s, value)))
			{
				return result;
			}

			return defaultValueFactory.Invoke();
		}

		public static TValue FirstOrDefault<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey needle)
		{
			return source
				.Where(kv => Equals(kv.Key, needle))
				.Select(kv => kv.Value)
				.FirstOrDefault();
		}

		public static TValue FirstOrDefault<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey> comparer, [CanBeNull] TKey needle)
		{
			return source
				.Where(kv => comparer.Equals(kv.Key, needle))
				.Select(kv => kv.Value)
				.FirstOrDefault();
		}

		public static IEnumerable<TSource> Do<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
		{
			foreach (var v in source)
			{
				action.Invoke(v);

				yield return v;
			}
		}

		public static IEnumerable<TSource> Prepend<TSource>(this IEnumerable<TSource> source, TSource value)
		{
			yield return value;

			foreach (var v in source)
			{
				yield return v;
			}
		}

		public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> source, TSource value)
		{
			foreach (var v in source)
			{
				yield return v;
			}

			yield return value;
		}

		public static IEnumerable<TSource> Safe<TSource>(this IEnumerable<TSource> source)
		{
			return source.Default(Enumerable.Empty<TSource>);
		}

		public static bool Require<TSource>(this IEnumerable<TSource> source, int count)
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

		public static void Distribute<TSource>(this IEnumerable<TSource> source, params Action<TSource>[] actions)
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

		public static void DistributeWithOverflow<TSource>(this IEnumerable<TSource> source, Action<TSource> overflow, params Action<TSource>[] actions)
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