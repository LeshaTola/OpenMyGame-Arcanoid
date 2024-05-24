using TMPro;
using UnityEngine;

namespace Module.Localization.Localizers
{
	public class TMProLocalizer : MonoBehaviour, ITextLocalizer
	{
		[SerializeField] private TextMeshProUGUI text;
		[SerializeField] private ILocalizationSystem localizationSystem;

		private string key = "";

		public string Key { get => key; set => key = value; }
		public string Text { get => text.text; set => text.text = value; }

		public void Init(ILocalizationSystem localizationSystem)
		{
			if (this.localizationSystem != null)
			{
				return;
			}

			this.localizationSystem = localizationSystem;
			key = text.text;
			localizationSystem.OnLanguageChanged += OnLanguageChanged;
		}

		private void OnDestroy()
		{
			localizationSystem.OnLanguageChanged -= OnLanguageChanged;
		}

		public void Translate()
		{
			if (!localizationSystem.LanguageDictionary.ContainsKey(key))
			{
				text.text = key;
				return;
			}
			string newText = localizationSystem.LanguageDictionary[key];
			text.text = newText;
		}

		private void OnLanguageChanged()
		{
			Translate();
		}
	}
}