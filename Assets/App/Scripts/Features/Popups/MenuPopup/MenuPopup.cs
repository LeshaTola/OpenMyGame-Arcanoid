using Features.Popups.Languages;
using Features.Popups.Menu.ViewModels;
using Module.Localization.Localizers;
using Module.PopupLogic.General.Popups;
using UnityEngine;

namespace Features.Popups.Menu
{
	public class MenuPopup : Popup
	{
		[SerializeField] private TMProLocalizer header;
		[SerializeField] private PopupButton restartButton;
		[SerializeField] private PopupButton backButton;
		[SerializeField] private PopupButton resumeButton;


		private IMenuPopupViewModel viewModel;

		public void Setup(IMenuPopupViewModel viewModel)
		{
			CleanUp();
			Initialize(viewModel);
			SetupLogic(viewModel);
			Translate();
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
			if (viewModel != null)
			{
				restartButton.onButtonClicked -= viewModel.RestartCommand.Execute;
				backButton.onButtonClicked -= viewModel.BackCommand.Execute;
				resumeButton.onButtonClicked -= viewModel.ResumeCommand.Execute;
			}
		}
	}
}