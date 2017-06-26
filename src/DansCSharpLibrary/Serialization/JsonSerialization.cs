﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DansCSharpLibrary.Serialization
{
	/// <summary>
	/// Functions for performing common Json Serialization operations.
	/// <para>Requires the Newtonsoft.Json assembly (Json.Net package in NuGet Gallery) to be referenced in your project.</para>
	/// <para>Only public properties and variables will be serialized.</para>
	/// <para>Use the [JsonIgnore] attribute to ignore specific public properties or variables.</para>
	/// <para>Object to be serialized must have a parameterless constructor.</para>
	/// </summary>
	public static class JsonSerialization
	{
		/// <summary>
		/// Writes the given object instance to a Json file.
		/// <para>Object type must have a parameterless constructor.</para>
		/// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
		/// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [JsonIgnore] attribute.</para>
		/// </summary>
		/// <typeparam name="T">The type of object being written to the file.</typeparam>
		/// <param name="filePath">The file path to write the object instance to.</param>
		/// <param name="objectToWrite">The object instance to write to the file.</param>
		/// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
		public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
		{
			TextWriter writer = null;
			try
			{
				var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
				writer = new StreamWriter(filePath, append);
				writer.Write(contentsToWriteToFile);
			}
			finally
			{
				if (writer != null)
					writer.Close();
			}
		}

		/// <summary>
		/// Reads an object instance from an Json file.
		/// <para>Object type must have a parameterless constructor.</para>
		/// </summary>
		/// <typeparam name="T">The type of object to read from the file.</typeparam>
		/// <param name="filePath">The file path to read the object instance from.</param>
		/// <returns>Returns a new instance of the object read from the Json file.</returns>
		public static T ReadFromJsonFile<T>(string filePath) where T : new()
		{
			TextReader reader = null;
			try
			{
				reader = new StreamReader(filePath);
				var fileContents = reader.ReadToEnd();
				return JsonConvert.DeserializeObject<T>(fileContents);
			}
			finally
			{
				if (reader != null)
					reader.Close();
			}
		}

		/// <summary>
		/// Serializes the instance to a JSON string.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="instance">The instance to serialize.</param>
		/// <returns></returns>
		public static string SerializeToString<T>(T instance)
		{
			return JsonConvert.SerializeObject(instance);
		}

		/// <summary>
		/// Deserializes an instance from the given JSON string.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="instanceString">The instance XML string.</param>
		/// <returns></returns>
		public static T DeserializeFromString<T>(string instanceString)
		{
			return JsonConvert.DeserializeObject<T>(instanceString);
		}
	}
}
