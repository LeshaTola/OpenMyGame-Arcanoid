using System;
using System.Collections.Generic;
using UnityEngine;

namespace Module.Localization.Parsers
{
	public class CSVParser : IParser
	{
		private char lineSeparator = '\n';
		private string surround = "\"";
		private string[] fieldSeparator = { "\",\"" };

		public Dictionary<string, string> Parse(string language, string localizationFile)
		{

			string[] lines = localizationFile.Split(lineSeparator);
			string[] headers = lines[0].Split(fieldSeparator, StringSplitOptions.None);

			int languageIndex = GetLanguageIndex(language, headers);
			if (languageIndex == -1)
			{
				Debug.LogError($"Can't find language {language}");
				return null;
			}
			Dictionary<string, string> parsedLanguage = GetDictionary(lines, languageIndex);

			return parsedLanguage;
		}

		public List<string> GetLanguages(string localizationFile)
		{
			List<string> languages = new();
			string[] lines = localizationFile.Split(lineSeparator);
			string[] headers = lines[0].Split(fieldSeparator, StringSplitOptions.None);

			for (int i = 1; i < headers.Length; i++)
			{
				languages.Add(headers[i]);
			}
			return languages;
		}

		private Dictionary<string, string> GetDictionary(string[] lines, int languageIndex)
		{
			//Regex regex = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

			Dictionary<string, string> parsedLanguage = new();
			for (int i = 1; i < lines.Length; i++)
			{
				string line = lines[i];

				string[] fields = line.Split(',');
				for (int j = 0; j < fields.Length; j++)
				{
					fields[j] = removeQuotes(fields[j]);
				}

				string key = fields[0];
				if (parsedLanguage.ContainsKey(key))
				{
					continue;
				}
				string value = fields[languageIndex];
				parsedLanguage.Add(key, value);
			}

			return parsedLanguage;
		}

		string removeQuotes(string s)
		{
			return s.Replace(surround, "");
		}

		private int GetLanguageIndex(string language, string[] headers)
		{
			int languageIndex = -1;

			for (int i = 0; i < headers.Length; i++)
			{
				if (headers[i].Equals(language))
				{
					languageIndex = i;
					break;
				}
			}

			return languageIndex;
		}
	}
}