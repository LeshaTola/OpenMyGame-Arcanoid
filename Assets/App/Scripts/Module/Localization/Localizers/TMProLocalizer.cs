using Features.Bootstrap;
using TMPro;
using UnityEngine;

namespace Module.Localization.Localizers
{
	public class TMProLocalizer : MonoBehaviour, IInitializable, ITextLocalizer
	{
		[SerializeField] private TextMeshProUGUI text;
		[SerializeField] private LocalizationSystem localizationSystem;

		private string key = "";

		public void Init()
		{
			key = text.text;
			localizationSystem.OnLanguageChanged += OnLanguageChanged;
		}

		public void Init(LocalizationSystem localizationSystem)
		{
			this.localizationSystem = localizationSystem;
			Init();
		}

		private void OnValidate()
		{
			TryGetComponent(out text);
			localizationSystem = FindAnyObjectByType<LocalizationSystem>();
		}

		private void OnDestroy()
		{
			localizationSystem.OnLanguageChanged -= OnLanguageChanged;
		}

		public void Translate()
		{
			string newText = localizationSystem.LanguageDictionary[key];
			text.text = newText;
		}

		private void OnLanguageChanged()
		{
			Translate();
		}
	}
}