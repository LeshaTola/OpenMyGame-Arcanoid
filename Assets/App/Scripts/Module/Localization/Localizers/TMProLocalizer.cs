using TMPro;
using UnityEngine;

namespace Module.Localization.Localizers
{
	public class TMProLocalizer : MonoBehaviour, ITextLocalizer
	{
		[SerializeField] private TextMeshProUGUI text;
		[SerializeField] private ILocalizationSystem localizationSystem;

		private string key = "";

		public string Text { get => text.text; set => text.text = value; }

		public void Init(ILocalizationSystem localizationSystem)
		{
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