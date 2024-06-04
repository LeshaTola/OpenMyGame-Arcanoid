using Cysharp.Threading.Tasks;
using Features.Popups.Languages;
using Features.Popups.Menu.ViewModels;
using Module.Localization.Localizers;
using Module.PopupLogic.General.Popups;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Popups.Menu
{
	public class MenuPopup : Popup
	{
		[SerializeField] private TMProLocalizer header;
		[SerializeField] private PopupButton restartButton;
		[SerializeField] private PopupButton backButton;
		[SerializeField] private PopupButton resumeButton;
		[SerializeField] private float animationDuration = 0.1f;

		private IMenuPopupViewModel viewModel;

		public void Setup(IMenuPopupViewModel viewModel)
		{
			viewModel.PopupAnimator.Setup(
				header,
				new List<PopupButton>
				{
					restartButton,
					backButton,
					resumeButton
				},
				animationDuration);
			viewModel.PopupAnimator.ResetAnimation();
			CleanUp();
			Initialize(viewModel);
			SetupLogic(viewModel);
			Translate();
			viewModel.PopupAnimator.ShowAnimation();
		}

		public async override UniTask Hide()
		{
			Deactivate();

			await viewModel.PopupAnimator.HideAnimation();
			await popupAnimation.Value.Hide();

			Controller.RemoveActivePopup(this);
			gameObject.SetActive(false);
		}

		private void SetupLogic(IMenuPopupViewModel viewModel)
		{
			restartButton.onButtonClicked += viewModel.RestartCommand.Execute;
			backButton.onButtonClicked += viewModel.BackCommand.Execute;
			resumeButton.onButtonClicked += viewModel.ResumeCommand.Execute;

			restartButton.UpdateText(viewModel.RestartCommand.Label);
			backButton.UpdateText(viewModel.BackCommand.Label);
			resumeButton.UpdateText(viewModel.ResumeCommand.Label);
		}

		private void Initialize(IMenuPopupViewModel viewModel)
		{
			this.viewModel = viewModel;
			header.Init(viewModel.LocalizationSystem);

			restartButton.Init(viewModel.LocalizationSystem);
			backButton.Init(viewModel.LocalizationSystem);
			resumeButton.Init(viewModel.LocalizationSystem);
		}

		private void Translate()
		{
			header.Translate();

			restartButton.Translate();
			backButton.Translate();
			resumeButton.Translate();
		}

		private void CleanUp()
		{
			if (viewModel == null)
			{
				return;
			}

			restartButton.onButtonClicked -= viewModel.RestartCommand.Execute;
			backButton.onButtonClicked -= viewModel.BackCommand.Execute;
			resumeButton.onButtonClicked -= viewModel.ResumeCommand.Execute;

		}
	}
}