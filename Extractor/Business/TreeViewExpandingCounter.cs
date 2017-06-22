using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Extractor.Business
{
	public static class TreeViewExpandingCounter
	{
		private static bool isCounting = false;
		public static bool IsCounting
		{
			get { return isCounting; }
		}

		private static int current = 0;
		public static int Current
		{
			get { return current; }
		}

		private static int total = 0;
		public static int Total
		{
			get { return total; }
		}

		public static void Set(int totalCount)
		{
			if (!isCounting)
			{
				total = totalCount;
				current = 0;
				isCounting = true;
			}
		}

		public static void Reset()
		{
			isCounting = false;
			current = 0;
			total = 0;
		}

		public static void StepForward()
		{
			if (isCounting)
			{
				if (checked(++current) == total)
				{
					Business.ProgressManager.Instance.UpdateProgress(1.0);
					Reset();
					if (Finish != null) Finish();
				}
				else
				{
					Business.ProgressManager.Instance.UpdateProgress((double)current / (double)total);
				}
			}
		}

		public static event Action Finish;
	}
}
