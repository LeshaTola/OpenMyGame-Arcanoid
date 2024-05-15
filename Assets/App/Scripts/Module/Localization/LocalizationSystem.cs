﻿using System.Collections.Generic;
using Features.Bootstrap;
using Module.Localization.Localizers;
using Module.Localization.Parsers;
using Sirenix.OdinInspector;
using TNRD;
using UnityEngine;

namespace Module.Localization
{
	public class LocalizationSystem : SerializedMonoBehaviour, IInitializable
	{
		[SerializeField] TextAsset localizationFile;
		[ValueDropdown("GetLanguages")]
		[SerializeField] private string language;
		[SerializeField] private List<SerializableInterface<ITextLocalizer>> textLocalizers;

		private Dictionary<string, Dictionary<string, string>> languagesDictionary;
		private IParser parser;

		public string Language { get => language; }
		public Dictionary<string, string> LanguageDictionary { get => languagesDictionary[language]; }

		public void Init()
		{
			languagesDictionary = new();
			parser = new CSVParser();
			var languages = parser.GetLanguages(localizationFile.text);

			foreach (var language in languages)
			{
				var parsedLanguage = parser.Parse(language, localizationFile.text);
				languagesDictionary.Add(language, parsedLanguage);
			}

			foreach (SerializableInterface<ITextLocalizer> textLocalizer in textLocalizers)
			{
				textLocalizer.Value.Init(this);
			}
			Translate();
		}

		public void Translate()
		{
			foreach (SerializableInterface<ITextLocalizer> textLocalizer in textLocalizers)
			{
				textLocalizer.Value.Translate();
			}
		}

#if UNITY_EDITOR
		private List<string> GetLanguages()
		{
			if (localizationFile == null)
			{
				return null;
			}

			if (parser == null)
			{
				parser = new CSVParser();
			}

			return parser.GetLanguages(localizationFile.text);
		}
#endif
	}
}