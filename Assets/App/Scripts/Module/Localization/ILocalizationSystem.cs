using System;
using System.Collections.Generic;

namespace Module.Localization
{
	public interface ILocalizationSystem
	{
		event Action OnLanguageChanged;

		string Language { get; }

		void ChangeLanguage(string languageKey);
		IEnumerable<string> GetLanguages();
		string Translate(string key);
	}
}