using System;
using System.IO;
using System.Text.RegularExpressions;

namespace MetaBuddy
{
	class Program
	{
		// replace this with your source directory
		//private static readonly string sourceDirectory = @"C:\Users\Chris\Desktop\test\";
		private static readonly string sourceDirectory = @"C:\Users\Username\videos\";

		static void Main(string[] args)
		{
			Console.WriteLine("Beginning to update metadata...");
			EditMetaData();
			Console.WriteLine("All done!");
		}

		private static void EditMetaData()
		{
			DirectoryInfo dir = new DirectoryInfo(sourceDirectory);
			string pattern = @"(- [0-9]+x[0-9]+ -)";
			foreach (var file in dir.GetFiles())
			{
				string fileName = Path.GetFileNameWithoutExtension(file.FullName);
				string[] parts = Regex.Split(fileName, pattern, RegexOptions.IgnoreCase);

				if (!Regex.IsMatch(fileName, pattern))
				{
					break;
				}

				var tagFile = TagLib.File.Create(file.FullName);
				tagFile.Tag.Title = parts[parts.Length - 1];
				tagFile.Save();
				;
			}
		}
	}
}
