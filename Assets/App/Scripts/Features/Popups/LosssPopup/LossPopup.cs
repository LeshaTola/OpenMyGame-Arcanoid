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

		private ILossPopupViewModel viewModel;

		public void Setup(ILossPopupViewModel viewModel)
		{
			CleanUp();

			Initialize(viewModel);

			restartButton.onButtonClicked += viewModel.RestartCommand.Execute;
			restartButton.UpdateText(viewModel.RestartCommand.Label);

			Translate();
		}

		private void Translate()
		{
			restartButton.Translate();
			header.Translate();
		}

		private void Initialize(ILossPopupViewModel viewModel)
		{
			this.viewModel = viewModel;
			restartButton.Init(viewModel.LocalizationSystem);
			header.Init(viewModel.LocalizationSystem);
		}

		private void CleanUp()
		{
			if (viewModel != null)
			{
				restartButton.onButtonClicked -= viewModel.RestartCommand.Execute;
			}
		}
	}
}