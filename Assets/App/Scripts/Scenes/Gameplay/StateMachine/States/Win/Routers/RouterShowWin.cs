using Features.Energy.Providers;
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
		private IEnergyProvider energyProvider;
		private IPackProvider packProvider;
		private LoadNextLevelCommand loadNextLevelCommand;

		public RouterShowWin(IPopupController popupController,
					   LoadNextLevelCommand loadNextLevelCommand,
					   ILocalizationSystem localizationSystem,
					   IEnergyProvider energyProvider,
					   IPackProvider packProvider)
		{
			this.popupController = popupController;
			this.loadNextLevelCommand = loadNextLevelCommand;
			this.localizationSystem = localizationSystem;
			this.energyProvider = energyProvider;
			this.packProvider = packProvider;
		}

		public async void ShowWin()
		{
			WinPopup popup = popupController.GetPopup<WinPopup>();
			SetupCommand();

			WinPopupViewModel popupViewModel = new(loadNextLevelCommand,
										  packProvider.CurrentPack,
										  packProvider.SavedPackData,
										  localizationSystem,
										  energyProvider);
			popup.Setup(popupViewModel);
			await popup.Show();
		}

		private void SetupCommand()
		{
			loadNextLevelCommand.IsNextLevel = true;
			if (packProvider.CurrentPack == null || packProvider.SavedPackData == null)
			{
				return;
			}

			if (packProvider.SavedPackData.CurrentLevel == packProvider.CurrentPack.MaxLevel)
			{
				if (packProvider.PackIndex == 0 || packProvider.SavedPackData.IsCompeted)
				{
					loadNextLevelCommand.IsNextLevel = false;
				}
			}
		}
	}
}
