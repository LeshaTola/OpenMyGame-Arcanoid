using Module.Localization.Localizers;
using Module.PopupLogic.General.Popups;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Popups.Languages
{
	public class InfoPopup : Popup
	{
		[SerializeField] private TMProLocalizer header;
		[SerializeField] private RectTransform buttonsContainer;

		private List<PopupButton> buttons = new();

		public void Setup(IGeneralPopupViewModel viewModel)
		{
			CleanUp();

			foreach (var command in viewModel.Commands)
			{
				PopupButton button = viewModel.ButtonsFactory.GetButton();

				button.transform.SetParent(buttonsContainer);
				button.transform.localScale = Vector2.one;

				button.Init(viewModel.LocalizationSystem);
				button.UpdateText(command.Label);
				button.onButtonClicked += command.Execute;

				buttons.Add(button);
			}

			Initialize(viewModel);
			Translate();
		}

		private void Translate()
		{
			header.Translate();
			foreach (var button in buttons)
			{
				button.Translate();
			}
		}

		private void Initialize(IGeneralPopupViewModel viewModel)
		{
			header.Init(viewModel.LocalizationSystem);
			header.Key = viewModel.Header;
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