using Module.Localization;
using Module.Localization.Localizers;
using System;
using UnityEngine;

namespace Features.Popups.Languages
{
	public class PopupButton : MonoBehaviour, IPopupButton
	{
		public event Action onButtonClicked;

		[SerializeField] private TMProLocalizer buttonText;
		[SerializeField] private UnityEngine.UI.Button button;

		public void UpdateText(string text)
		{
			buttonText.Key = text;
		}

		public void Init(ILocalizationSystem localizationSystem)
		{
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => onButtonClicked?.Invoke());
			buttonText.Init(localizationSystem);
		}

		public void Translate()
		{
			buttonText.Translate();
		}

	}
}