using Features.Popups.Languages;
using Features.Popups.Loss.ViewModels;
using Module.Localization.Localizers;
using Module.PopupLogic.General.Popups;
using UnityEngine;

namespace Features.Popups.Loss
{
	public class LossPopup : Popup
	{
		[SerializeField] private TMProLocalizer header;

		[SerializeField] private PopupButton restartButton;
		[SerializeField] private PopupButton backButton;
		[SerializeField] private PopupButton continueButton;

		private ILossPopupViewModel viewModel;

		public void Setup(ILossPopupViewModel viewModel)
		{
			CleanUp();
			Initialize(viewModel);
			SetupLogic(viewModel);
			Translate();
		}

		private void SetupLogic(ILossPopupViewModel viewModel)
		{
			restartButton.onButtonClicked += viewModel.RestartCommand.Execute;
			restartButton.UpdateText(viewModel.RestartCommand.Label);

			continueButton.onButtonClicked += viewModel.ContinueCommand.Execute;
			continueButton.UpdateText(viewModel.ContinueCommand.Label);

			backButton.onButtonClicked += viewModel.BackCommand.Execute;
			backButton.UpdateText(viewModel.BackCommand.Label);
		}

		private void Translate()
		{
			header.Translate();

			restartButton.Translate();
			continueButton.Translate();
			backButton.Translate();
		}

		private void Initialize(ILossPopupViewModel viewModel)
		{
			this.viewModel = viewModel;
			header.Init(viewModel.LocalizationSystem);

			restartButton.Init(viewModel.LocalizationSystem);
			continueButton.Init(viewModel.LocalizationSystem);
			backButton.Init(viewModel.LocalizationSystem);
		}

		private void CleanUp()
		{
			if (viewModel != null)
			{
				restartButton.onButtonClicked -= viewModel.RestartCommand.Execute;
				continueButton.onButtonClicked -= viewModel.ContinueCommand.Execute;
				backButton.onButtonClicked -= viewModel.BackCommand.Execute;
			}
		}
	}
}