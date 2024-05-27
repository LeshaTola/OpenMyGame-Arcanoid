using Module.Localization;
using System;

namespace Features.Popups.Languages
{
	public interface IPopupButton
	{
		event Action onButtonClicked;

		void Init(ILocalizationSystem localizationSystem);
		void Translate();
		void UpdateText(string text);
	}
}