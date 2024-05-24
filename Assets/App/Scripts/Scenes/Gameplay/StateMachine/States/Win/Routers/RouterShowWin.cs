using Features.Popups.WinPopup;
using Features.Popups.WinPopup.ViewModels;
using Module.Localization;
using Module.PopupLogic.General.Controller;
using Scenes.Gameplay.Feature.Commands;
using Scenes.PackSelection.Feature.Packs;

namespace Scenes.Gameplay.StateMachine.States.Win.Routers
{
	public class RouterShowWin : IRouterShowWin
	{
		private IPopupController popupController;
		private ILocalizationSystem localizationSystem;
		private IPackProvider packProvider;
		private LoadNextLevelCommand loadNextLevelCommand;

		public RouterShowWin(IPopupController popupController,
					   IPackProvider packProvider,
					   LoadNextLevelCommand loadNextLevelCommand,
					   ILocalizationSystem localizationSystem)
		{
			this.popupController = popupController;
			this.packProvider = packProvider;
			this.loadNextLevelCommand = loadNextLevelCommand;
			this.localizationSystem = localizationSystem;
		}

		public void ShowWin()
		{
			WinPopup popup = popupController.GetPopup<WinPopup>();
			WinPopupViewModel popupViewModel = new(loadNextLevelCommand,
										  packProvider.CurrentPack,
										  packProvider.SavedPackData,
										  localizationSystem);
			popup.Setup(popupViewModel);
			popup.Show();
		}
	}
}
