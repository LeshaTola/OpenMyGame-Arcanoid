using Features.Bootstrap;
using Module.Localization.Configs;
using Module.Localization.Parsers;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Module.Localization
{
	public class LocalizationSystem : SerializedMonoBehaviour, IInitializable
	{
		public event Action OnLanguageChanged;

		[SerializeField] LocalizationDictionary localizationDictionary;
		[ValueDropdown("GetLanguages")]
		[SerializeField] private string language;

		private Dictionary<string, Dictionary<string, string>> languagesDictionary;
		private IParser parser;

		public string Language { get => language; }
		public Dictionary<string, string> LanguageDictionary { get => languagesDictionary[language]; }

		public void Init()
		{
			languagesDictionary = new();
			parser = new CSVParser();

			foreach (var language in localizationDictionary.Languages)
			{
				var parsedLanguage = parser.Parse(language.SCVFile.text);
				languagesDictionary.Add(language.LanguageName, parsedLanguage);
			}
			ChangeLanguage();
		}

		public void ChangeLanguage()
		{
			OnLanguageChanged?.Invoke();
		}

#if UNITY_EDITOR
		private List<string> GetLanguages()
		{
			if (localizationDictionary == null)
			{
				return null;
			}

			return localizationDictionary.Languages.Select(language => language.LanguageName).ToList();
		}
#endif
	}
}