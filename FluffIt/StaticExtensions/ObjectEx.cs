using System.Collections.Generic;

namespace FluffIt.StaticExtensions
{
	public static class ObjectEx
	{
		/// <summary>
		/// Checks for equality between two values using an IEqualityComparer.
		/// </summary>
		/// <typeparam name="T">Type of values to be compared</typeparam>
		/// <param name="left">First value to be compared</param>
		/// <param name="right">Second value to be compared</param>
		/// <param name="comparer">Equality comparer to use. Optional</param>
		/// <returns>Returns true if both values are equal acording to the equality comparer, if no comparer is provided, fallback to the default comparer</returns>
		public static bool Equals<T>(T left, T right, IEqualityComparer<T> comparer)
		{
			return comparer.SelectOrDefault(e => e.Equals(left, right), () => Equals(left, right));
		}
	}
}