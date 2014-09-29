using System;
using System.Collections.Generic;

namespace Fluff.Extensions
{
	public static class ObjectExtensions
	{
		public static bool IsNullOrDefault<TSource>(this TSource source,  IEqualityComparer<TSource> valueTypeComparer = null)
		{
			return
				ReferenceEquals(source, null) ||
				valueTypeComparer == null
					? Equals(source, default(TSource))
					: valueTypeComparer.Equals(source, default(TSource));
		}

		public static TSource Default<TSource>(this TSource source, Func<TSource> defaultValueFactory, IEqualityComparer<TSource> valueTypeComparer = null)
		{
			return source.IsNullOrDefault(valueTypeComparer)
				? defaultValueFactory.Invoke()
				: source;
		}

		public static TSource Default<TSource>(this TSource source, TSource defaultValue, IEqualityComparer<TSource> valueTypeComparer = null)
		{
			return source.IsNullOrDefault(valueTypeComparer)
				? defaultValue
				: source;
		}

		public static TResult SelectOrDefault<TSource, TResult>(this TSource source, Func<TSource, TResult> selector, IEqualityComparer<TSource> valueTypeComparer = null)
		{
			return source.IsNullOrDefault(valueTypeComparer)
				? default(TResult)
				: selector.Invoke(source);
		}

		public static TResult SelectOrDefault<TSource, TResult>(
			this TSource source,
			Func<TSource, TResult> selector,
			Func<TResult> defaultValueFactory,
			IEqualityComparer<TSource> valueTypeComparer = null
		)
		{
			return source.IsNullOrDefault(valueTypeComparer)
				? defaultValueFactory.Invoke()
				: selector.Invoke(source);
		}

		public static TResult SelectOrDefault<TSource, TResult>(
			this TSource source,
			Func<TSource, TResult> selector,
			TResult defaultValue,
			IEqualityComparer<TSource> valueTypeComparer = null
		)
		{
			return source.IsNullOrDefault(valueTypeComparer)
				? defaultValue
				: selector.Invoke(source);
		}

		public static void Maybe<TSource>(this TSource source, Action<TSource> maybe, IEqualityComparer<TSource> valueTypeComparer = null)
		{
			if (!source.IsNullOrDefault(valueTypeComparer))
			{
				maybe.Invoke(source);
			}
		}

		/// <summary>
		/// Cast an input value to a target type and returns the new value.
		/// Simiar to the "as" operator but works on value types.
		/// </summary>
		/// <typeparam name="TResult">The type to cast to</typeparam>
		/// <param name="source">The source value to cast</param>
		/// <returns>The value casted into the specified type or the default value for the destination type if the types are incompatibles</returns>
		public static TResult As<TResult>(this object source)
		{
			return source is TResult
				? (TResult)source
				: default(TResult);
		}

		/// <summary>
		/// Cast an input value to a target type and call a specified action on success.
		/// Mostly used to create the equivalent of a switch on the type of a value.
		/// </summary>
		/// <typeparam name="TResult">The type to cast to</typeparam>
		/// <param name="source">The source value to cast</param>
		/// <param name="maybe">The action to execute if the cast is successfull</param>
		/// <returns>The original, unconverted value</returns>
		public static object As<TResult>(this object source, Action<TResult> maybe)
		{
			if (source is TResult)
			{
				maybe.Invoke((TResult)source);
			}

			return source;
		}

		public static void DoubleCheckedLocked<TSource>(this TSource source, Func<TSource, bool> check, Action<TSource> lockedAction)
			where TSource : class
		{
			if (check.Invoke(source))
			{
				lock (source)
				{
					if (check.Invoke(source))
					{
						lockedAction.Invoke(source);
					}
				}
			}
		}
	}
}