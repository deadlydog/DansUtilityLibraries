﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace DansCSharpLibrary.Serialization
{
	/// <summary>
	/// Functions for performing common binary Serialization operations.
	/// <para>All properties and variables will be serialized.</para>
	/// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
	/// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
	/// </summary>
	public static class BinarySerialization
	{
		/// <summary>
		/// Writes the given object instance to a binary file.
		/// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
		/// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
		/// </summary>
		/// <typeparam name="T">The type of object being written to the XML file.</typeparam>
		/// <param name="filePath">The file path to write the object instance to.</param>
		/// <param name="objectToWrite">The object instance to write to the XML file.</param>
		/// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
		public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
		{
			using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
			{
				var binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(stream, objectToWrite);
			}
		}

		/// <summary>
		/// Reads an object instance from a binary file.
		/// </summary>
		/// <typeparam name="T">The type of object to read from the XML.</typeparam>
		/// <param name="filePath">The file path to read the object instance from.</param>
		/// <returns>Returns a new instance of the object read from the binary file.</returns>
		public static T ReadFromBinaryFile<T>(string filePath)
		{
			using (Stream stream = File.Open(filePath, FileMode.Open))
			{
				var binaryFormatter = new BinaryFormatter();
				return (T)binaryFormatter.Deserialize(stream);
			}
		}

		/// <summary>
		/// Serializes the given instance to a string.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns></returns>
		public static string SerializeToString<T>(T instance)
		{
			using (var stream = new MemoryStream())
			{
				var formatter = new BinaryFormatter();
				formatter.Serialize(stream, instance);
				stream.Flush();
				stream.Position = 0;
				return Convert.ToBase64String(stream.ToArray());
			}
		}

		/// <summary>
		/// Deserializes a new instance from the given serialized string.
		/// </summary>
		/// <param name="instanceString">The instance string.</param>
		/// <returns></returns>
		public static T DeserializeFromString<T>(string instanceString)
		{
			byte[] byteArray = Convert.FromBase64String(instanceString);
			using (var stream = new MemoryStream(byteArray))
			{
				var formatter = new BinaryFormatter();
				stream.Seek(0, SeekOrigin.Begin);
				return (T)formatter.Deserialize(stream);
			}
		}
	}
}
