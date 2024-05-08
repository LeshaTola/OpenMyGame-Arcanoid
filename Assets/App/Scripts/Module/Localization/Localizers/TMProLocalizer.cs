using TMPro;
using UnityEngine;

namespace Module.Localization.Localizers
{
	public class TMProLocalizer : MonoBehaviour, ITextLocalizer
	{
		[SerializeField] private TextMeshProUGUI text;

		private string key = "";
		private LocalizationSystem localizationSystem;

		public void Init(LocalizationSystem localizationSystem)
		{
			this.localizationSystem = localizationSystem;
			key = text.text;
		}

		private void OnValidate()
		{
			TryGetComponent(out text);
		}

		public void Translate()
		{
			string newText = localizationSystem.LanguageDictionary[key];
			text.text = newText;
		}
	}
}