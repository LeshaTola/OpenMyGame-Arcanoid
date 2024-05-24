using Features.Popups.WinPopup;
using Features.Popups.WinPopup.ViewModels;
using Module.PopupLogic.General.Controller;
using Scenes.Gameplay.Feature.Commands;
using Scenes.PackSelection.Feature.Packs;

namespace Scenes.Gameplay.StateMachine.States.Win.Routers
{
	public class RouterShowWin : IRouterShowWin
	{
		private IPopupController popupController;
		private IPackProvider packProvider;
		private LoadNextLevelCommand loadNextLevelCommand;

		public RouterShowWin(IPopupController popupController,
					   IPackProvider packProvider,
					   LoadNextLevelCommand loadNextLevelCommand)
		{
			this.popupController = popupController;
			this.packProvider = packProvider;
			this.loadNextLevelCommand = loadNextLevelCommand;
		}

		public void ShowWin()
		{
			WinPopup popup = popupController.GetPopup<WinPopup>();
			WinPopupViewModel popupViewModel = new(loadNextLevelCommand, packProvider.CurrentPack, packProvider.SavedPackData);
			popup.Setup(popupViewModel);
			popup.Show();
		}
	}
}
