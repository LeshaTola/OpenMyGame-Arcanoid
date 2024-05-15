using System;
using System.Collections.Generic;

namespace Module.Localization.Parsers
{
	public class CSVParser : IParser
	{
		private char lineSeparator = '\n';
		private string surround = "\"";
		private string fieldSeparator = "\",\"";

		public Dictionary<string, string> Parse(string localizationFile)
		{

			string[] lines = localizationFile.Split(lineSeparator);
			string[] headers = lines[0].Split(fieldSeparator, StringSplitOptions.None);

			Dictionary<string, string> parsedLanguage = GetDictionary(lines);

			return parsedLanguage;
		}

		private Dictionary<string, string> GetDictionary(string[] lines)
		{
			//Regex regex = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

			Dictionary<string, string> parsedLanguage = new();
			for (int i = 1; i < lines.Length; i++)
			{
				string line = lines[i];

				string[] fields = line.Split(fieldSeparator);
				for (int j = 0; j < fields.Length; j++)
				{
					fields[j] = removeQuotes(fields[j]);
				}

				string key = fields[0];
				if (parsedLanguage.ContainsKey(key))
				{
					continue;
				}
				string value = fields[1];
				parsedLanguage.Add(key, value);
			}

			return parsedLanguage;
		}

		string removeQuotes(string s)
		{
			return s.Replace(surround, "");
		}
	}
}