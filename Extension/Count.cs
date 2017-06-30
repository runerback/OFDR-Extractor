
namespace System.Linq
{
	public static class CountExtension
	{
		public static int Count(this System.Collections.IEnumerable source)
		{
			var enumerator = source.GetEnumerator();
			int count = 0;
			while (enumerator.MoveNext())
			{
				count++;
			}
			return count;
		}
	}
}
