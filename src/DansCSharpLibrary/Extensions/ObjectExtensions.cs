using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DansCSharpLibrary.Diagnostics;

namespace DansCSharpLibrary.Extensions
{
	public static class ObjectExtensions
	{
		public static string GetDiagnosticInformation(this object instance)
		{

			var diagnosticProvider = instance as IProvideDiagnosticInformation ?? new ReflectionBasedDiagnosticInformationProvider(instance);
			return diagnosticProvider.GetDiagnosticInformation();
		}
	}
}
