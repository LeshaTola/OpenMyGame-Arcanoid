using Features.Popups.WinPopup;
using Features.Popups.WinPopup.ViewModels;
using Features.Saves;
using Module.Localization;
using Module.PopupLogic.General.Controller;
using Scenes.Gameplay.Feature.Commands;
using Scenes.PackSelection.Feature.Packs.Configs;

namespace Scenes.Gameplay.StateMachine.States.Win.Routers
{
	public class RouterShowWin : IRouterShowWin
	{
		private IPopupController popupController;
		private ILocalizationSystem localizationSystem;
		private LoadNextLevelCommand loadNextLevelCommand;

		public RouterShowWin(IPopupController popupController,
					   LoadNextLevelCommand loadNextLevelCommand,
					   ILocalizationSystem localizationSystem)
		{
			this.popupController = popupController;
			this.loadNextLevelCommand = loadNextLevelCommand;
			this.localizationSystem = localizationSystem;
		}

		public void ShowWin(Pack currentPack, SavedPackData savedPackData)
		{
			WinPopup popup = popupController.GetPopup<WinPopup>();
			SetupCommand(currentPack, savedPackData);

			WinPopupViewModel popupViewModel = new(loadNextLevelCommand,
										  currentPack,
										  savedPackData,
										  localizationSystem);
			popup.Setup(popupViewModel);
			popup.Show();
		}

		private void SetupCommand(Pack currentPack, SavedPackData savedPackData)
		{
			loadNextLevelCommand.IsNextLevel = true;
			if (currentPack == null || savedPackData == null)
			{
				return;
			}

			if (savedPackData.CurrentLevel == currentPack.MaxLevel && savedPackData.IsCompeted)
			{
				loadNextLevelCommand.IsNextLevel = false;
			}
		}
	}
}
