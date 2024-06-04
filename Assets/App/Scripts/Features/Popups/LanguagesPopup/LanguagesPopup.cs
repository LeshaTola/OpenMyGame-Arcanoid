using Cysharp.Threading.Tasks;
using Module.Localization.Localizers;
using Module.PopupLogic.General.Popups;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Popups.Languages
{
	public class LanguagesPopup : Popup
	{
		[SerializeField] private TMProLocalizer header;
		[SerializeField] private RectTransform buttonsContainer;
		[SerializeField] private float animationDuration = 0.15f;

		private List<PopupButton> buttons = new();
		private IGeneralPopupViewModel viewModel;

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
				button.Translate();

				buttons.Add(button);
			}
			header.Init(viewModel.LocalizationSystem);
			header.Key = viewModel.Header;
			header.Translate();

			this.viewModel = viewModel;
			viewModel.PopupAnimator.Setup(header, buttons, animationDuration);
			viewModel.PopupAnimator.ResetAnimation();
		}

		public async override UniTask Show()
		{
			await base.Show();
			await viewModel.PopupAnimator.ShowAnimation();

		}

		public async override UniTask Hide()
		{
			await viewModel.PopupAnimator.HideAnimation();
			await base.Hide();
		}

		private void CleanUp()
		{
			foreach (Transform child in buttonsContainer)
			{
				Destroy(child.gameObject);
			}
			buttons.Clear();
		}
	}
}