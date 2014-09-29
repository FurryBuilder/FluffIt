using System.Collections.Generic;
using System.Text;

namespace Fluff.Extensions
{
	public static class StringBuilderExtensions
	{
		public static IEnumerable<char> ToEnumerable(this StringBuilder builder)
		{
			for (var i = 0; i < builder.Length; ++i)
			{
				yield return builder[i];
			}
		}
	}
}