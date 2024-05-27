using UnityEngine;

namespace Features.Popups.Languages
{
	public class ButtonsFactory : IButtonsFactory
	{
		private PopupButton buttonTemplate;

		public ButtonsFactory(PopupButton buttonTemplate)
		{
			this.buttonTemplate = buttonTemplate;
		}

		public PopupButton GetButton()
		{
			return GameObject.Instantiate(buttonTemplate);
		}
	}
}