using System.Web;
using System.Runtime.CompilerServices;
using System.Linq;

namespace CimexUtility.Miscellaneous
{
	public static class VariableManipulation
	{
		/// <summary>
		/// Switches one variable to equal the other, thread safe.
		/// </summary>
		/// <typeparam name="T">The type of the variables.</typeparam>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void FlipVariables<T>(ref T a, ref T b)
		{
			var temp = a;
			a = b;
			b = temp;
		}
	}
}