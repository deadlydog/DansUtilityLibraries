using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DansCSharpLibrary.Configuration
{
	public class ConfigurationManager
	{
		/// <summary>
		/// Gets the value from application settings from the app/web.config file, or throws a System.Configuration.ConfigurationErrorsException exception if it is not found.
		/// </summary>
		/// <param name="appSettingKey">Name of the application setting key in the app/web.config file.</param>
		/// <returns></returns>
		/// <exception cref="System.Configuration.ConfigurationErrorsException"></exception>
		public static string GetValueFromAppSettingsOrThrowException(string appSettingKey)
		{
			if (System.Configuration.ConfigurationManager.AppSettings[appSettingKey] == null)
				throw new ConfigurationErrorsException(string.Format("Could not find the application setting '{0}' in the app.config file.", appSettingKey));
			return System.Configuration.ConfigurationManager.AppSettings[appSettingKey];
		}
	}
}
