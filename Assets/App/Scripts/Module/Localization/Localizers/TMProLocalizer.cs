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
		public TextMeshProUGUI Text { get => text; set => text = value; }

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
			string newText = localizationSystem.Translate(key);
			text.text = newText;
		}

		private void OnLanguageChanged()
		{
			Translate();
		}
	}
}