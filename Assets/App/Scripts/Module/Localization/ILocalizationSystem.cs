using System;
using System.Collections.Generic;

namespace Module.Localization
{
	public interface ILocalizationSystem
	{
		string Language { get; }

		Dictionary<string, string> LanguageDictionary { get; }

		event Action OnLanguageChanged;

		void ChangeLanguage(string languageKey);

		IEnumerable<string> GetLanguages();
	}
}