using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
				var errorMessage = string.Format("Could not find the appSetting key '{0}' in the app/web.config file.", appSettingKey);
				throw new System.Configuration.ConfigurationErrorsException(errorMessage, ex);
			}
		}

		/// <summary>
		/// Gets the integer value of a key in the appSettings section of the app/web.config file, or throws a System.Configuration.ConfigurationErrorsException exception if it is not found or cannot be converted to an integer.
		/// </summary>
		/// <param name="appSettingKey">Name of the application setting key in the app/web.config file.</param>
		/// <returns></returns>
		/// <exception cref="System.Configuration.ConfigurationErrorsException"></exception>
		public static int GetIntValueFromConfigurationManagerAppSettings(string appSettingKey)
		{
			var stringValue = GetValueFromConfigurationManagerAppSettings(appSettingKey);
			try
			{
				return int.Parse(stringValue);
			}
			catch (Exception ex)
			{
				var errorMessage = string.Format("Could not convert the value '{0}' of the appSetting key '{1}' in the app/web.config to an integer.", stringValue, appSettingKey);
				throw new System.Configuration.ConfigurationErrorsException(errorMessage, ex);
			}
		}

		/// <summary>
		/// Gets the boolean value of a key in the appSettings section of the app/web.config file, or throws a System.Configuration.ConfigurationErrorsException exception if it is not found or cannot be converted to a boolean.
		/// </summary>
		/// <param name="appSettingKey">Name of the application setting key in the app/web.config file.</param>
		/// <returns></returns>
		/// <exception cref="System.Configuration.ConfigurationErrorsException"></exception>
		public static bool GetBoolValueFromConfigurationManagerAppSettings(string appSettingKey)
		{
			var stringValue = GetValueFromConfigurationManagerAppSettings(appSettingKey);
			try
			{
				return bool.Parse(stringValue);
			}
			catch (Exception ex)
			{
				var errorMessage = string.Format("Could not convert the value '{0}' of the appSetting key '{1}' in the app/web.config to a boolean.", stringValue, appSettingKey);
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
				var errorMessage = string.Format("Could not find the connectionString name '{0}' in the app/web.config file.", connectionStringName);
				throw new System.Configuration.ConfigurationErrorsException(errorMessage, ex);
			}
		}
	}
}
