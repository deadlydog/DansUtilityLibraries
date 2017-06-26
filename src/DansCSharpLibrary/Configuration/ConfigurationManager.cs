using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DansCSharpLibrary.Extensions;

namespace DansCSharpLibrary.Configuration
{
	public class ConfigurationManager
	{
		/// <summary>
		/// Gets the value of a key in the appSettings section of the app/web.config file, or throws a System.Configuration.ConfigurationErrorsException exception if it is not found.
		/// </summary>
		/// <param name="appSettingKey">Name of the application setting key in the app/web.config file.</param>
		/// <returns></returns>
		/// <exception cref="System.Configuration.ConfigurationErrorsException"></exception>
		public static string GetValueFromConfigurationManagerAppSettings(string appSettingKey)
		{
			try
			{
				return System.Configuration.ConfigurationManager.AppSettings[appSettingKey];
			}
			catch (Exception ex)
			{
				var errorMessage = $"Could not find the appSetting key '{appSettingKey}' in the app/web.config file.";
				throw new System.Configuration.ConfigurationErrorsException(errorMessage, ex);
			}
		}

		/// <summary>
		/// Gets the value of a key in the appSettings section of the app/web.config file, or throws a System.Configuration.ConfigurationErrorsException exception if it is not found or cannot be converted to the specified type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="appSettingKey">Name of the application setting key in the app/web.config file.</param>
		/// <returns></returns>
		/// <exception cref="System.Configuration.ConfigurationErrorsException"></exception>
		public static T GetValueFromConfigurationManagerAppSettings<T>(string appSettingKey)
		{
			var stringValue = GetValueFromConfigurationManagerAppSettings(appSettingKey);
			try
			{
				return stringValue.ConvertTo<T>();
			}
			catch (Exception ex)
			{
				var errorMessage = $"Could not convert the value '{stringValue}' of the appSetting key '{appSettingKey}' in the app/web.config to type '{typeof(T).Name}'.";
				throw new System.Configuration.ConfigurationErrorsException(errorMessage, ex);
			}
		}

		/// <summary>
		/// Gets the connection string of a name in the connectionStrings section of the app/web.config file, or throws a System.Configuration.ConfigurationErrorsException exception if it is not found.
		/// </summary>
		/// <param name="connectionStringName">Name of the connection string in the app/web.config file.</param>
		/// <returns></returns>
		/// <exception cref="System.Configuration.ConfigurationErrorsException"></exception>
		public static string GetConnectionStringFromConfigurationManagerConnectionStrings(string connectionStringName)
		{
			try
			{
				var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName];
				return connectionString.ConnectionString;
			}
			catch (Exception ex)
			{
				var errorMessage = $"Could not find the connectionString name '{connectionStringName}' in the app/web.config file.";
				throw new System.Configuration.ConfigurationErrorsException(errorMessage, ex);
			}
		}
	}
}
