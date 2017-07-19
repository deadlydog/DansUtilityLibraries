using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DansCSharpLibrary.Diagnostics
{
	public class ReflectionBasedDiagnosticInformationProvider : IProvideDiagnosticInformation
	{
		private readonly object _instance;

		public ReflectionBasedDiagnosticInformationProvider(object instance)
		{
			if (instance == null)
				throw new ArgumentNullException(nameof(instance));

			_instance = instance;
		}

		public string GetDiagnosticInformation()
		{
			var infoBuilder = new StringBuilder();

			// Aggregate all of the public properties into a comma-separated string of name=value pairs.
			var publicProperties = _instance.GetType().GetProperties().Where(PropertyShouldBeListedInDiagnosticInfo);
			foreach (var property in publicProperties)
			{
				infoBuilder.Append($"{property.Name}='{property.GetValue(_instance)}',");
			}
			var info = infoBuilder.ToString().Trim(",".ToCharArray());

			return string.IsNullOrWhiteSpace(info) ?
				$"No diagnostic info to report on type '{_instance.GetType().FullName}' because no public properties were found." :
				$"Instance of type '{_instance.GetType().FullName}' has the following public property values (format is name='value'): {info}";
		}

		private static bool PropertyShouldBeListedInDiagnosticInfo(PropertyInfo property)
		{
			return property.PropertyType.IsPublic && PropertyHasNoParameters(property);
		}

		/// <remarks>
		/// A property with parameters is an indexer, such as <see cref="System.Data.DataRow.this[System.Data.DataColumn]"/>.
		/// </remarks>
		private static bool PropertyHasNoParameters(PropertyInfo property)
		{
			var indexParameters = property.GetIndexParameters();
			return indexParameters == null || !indexParameters.Any();
		}
	}
}
