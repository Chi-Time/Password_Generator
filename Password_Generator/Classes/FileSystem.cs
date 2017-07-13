using System;
using System.IO;
using Newtonsoft.Json;

namespace Password_Generator
{
	/// <summary>
	/// Utility class for reading and writing password information to JSON files.
	/// </summary>
	public static class FileSystem
	{
		/// <summary>
		/// Checks if the provided file actually exists in the directory.
		/// </summary>
		public static bool CheckFile (string file)
		{
			string filePath = GetFilePath (file);

			Console.WriteLine (filePath);

			if (File.Exists (filePath))
				return true;

			return false;
		}

		/// <summary>
		/// Reads a file and returns an object representing it's data.
		/// </summary>
		/// <returns>The object representing the data.</returns>
		/// <param name="file">The file to look for.</param>
		public static PasswordInfo ReadFromFile (string file)
		{
			string content = "";
			string filePath = GetFilePath (file);

			if (!File.Exists (filePath))
				return null;

			content = File.ReadAllText (filePath);

			var info = JsonConvert.DeserializeObject<PasswordInfo> (content);
			return info;
		}

		/// <summary>
		/// Finds and retrieves a path for the file specified.
		/// </summary>
		/// <returns> The path of the file. </returns>
		/// <param name="file"> The file to get a path for. </param>
		static string GetFilePath (string file)
		{
			string relativePath = @"Resources";
			string fileName = file + ".json";
			string filePath = Path.Combine (relativePath, fileName);

			return filePath;
		}

		/// <summary>
		/// Writes the provided password object to a json file.
		/// </summary>
		/// <param name="info">Info.</param>
		public static void WriteToFile (PasswordInfo info)
		{
			string relativePath = @"Resources";
			string filePath = GetFilePath (info.Site);

			if (!Directory.Exists(relativePath))
				Directory.CreateDirectory (relativePath);

			string content = JsonConvert.SerializeObject (info, Formatting.Indented);

			using (StreamWriter sw = File.CreateText (filePath))
			{
				sw.Write (content);
			}
		}

		/// <summary>
		/// Attempts to delete the provided file if it exists.
		/// </summary>
		/// <returns><c>true</c>, if file was deleted, <c>false</c> otherwise.</returns>
		/// <param name="file">File.</param>
		public static bool DeleteFile (string file)
		{
			string filePath = GetFilePath (file);

			if (File.Exists (filePath))
			{
				File.Delete (filePath);
				return true;
			}

			return false;
		}
	}
}
