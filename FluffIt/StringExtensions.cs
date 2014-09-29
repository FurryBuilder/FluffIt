namespace Fluff.Extensions
{
	public static class StringExtensions
	{
		public static bool IsNullOrEmpty(this string source)
		{
			return string.IsNullOrEmpty(source);
		}

		public static bool IsNullOrWhiteSpace(this string source)
		{
			return string.IsNullOrWhiteSpace(source);
		}

		public static string Format(this string source, params object[] args)
		{
			return string.Format(source, args);
		}

		public static string Safe(this string source)
		{
			return source.Default(string.Empty);
		}
	}
}