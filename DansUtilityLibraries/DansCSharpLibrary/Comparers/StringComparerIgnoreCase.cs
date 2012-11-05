using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DansCSharpLibrary.Comparers
{
	/// <summary>
	/// Comparer used to check if strings are the same while ignoring case.
	/// </summary>
	public class StringComparerIgnoreCase : IEqualityComparer<string>
	{
		public bool Equals(string x, string y)
		{
			return x.Equals(y, StringComparison.InvariantCultureIgnoreCase);
		}

		public int GetHashCode(string obj)
		{
			return obj.GetHashCode();
		}
	}
}
