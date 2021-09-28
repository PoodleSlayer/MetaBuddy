using System;
using System.IO;
using System.Text.RegularExpressions;

namespace MetaBuddy
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Enter a directory to look for matching video files:");
			string sourceDirectory = Console.ReadLine();
			Console.WriteLine("Beginning to update metadata...");
			int videoCount = EditMetaData(sourceDirectory);
			Console.WriteLine($"All done! Updated {videoCount} files");
			Console.ReadKey();
		}

		private static int EditMetaData(string sourceDirectory)
		{
			DirectoryInfo dir = new DirectoryInfo(sourceDirectory);
			string pattern = @"(- [0-9]+x[0-9]+ -)";
			int videoCount = 0;

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
				videoCount++;
			}

			return videoCount;
		}
	}
}
