using Module.Localization.Localizers;
using Module.PopupLogic.General.Popups;
using UnityEngine;

namespace Features.Popups.Languages
{
	public class LanguagesPopup : Popup
	{
		[SerializeField] private TMProLocalizer header;
		[SerializeField] private RectTransform buttonsContainer;

		public void Setup(IGeneralPopupViewModel viewModel)
		{
			CleanUp();
			header.Text = viewModel.Header;

			foreach (var command in viewModel.Commands)
			{
				PopupButton button = viewModel.ButtonsFactory.GetButton();
				button.transform.SetParent(buttonsContainer);
				button.transform.localScale = Vector2.one;
				button.Init(viewModel.LocalizationSystem);
				button.UpdateText(command.Label);
				button.onButtonClicked += command.Execute;
				button.Translate();
			}
			header.Init(viewModel.LocalizationSystem);
			header.Translate();
		}

		private void CleanUp()
		{
			foreach (Transform child in buttonsContainer)
			{
				Destroy(child.gameObject);
			}
		}
	}
}