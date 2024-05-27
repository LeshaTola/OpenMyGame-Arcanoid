using Features.Energy.Providers;
using Features.Saves;
using Module.Commands;
using Module.Localization;
using Scenes.PackSelection.Feature.Packs.Configs;

namespace Features.Popups.WinPopup.ViewModels
{
	public class WinPopupViewModel : IWinPopupViewModel
	{
		public WinPopupViewModel(ILabeledCommand loadNextLevelCommand,
						   Pack pack,
						   SavedPackData savedPackData,
						   ILocalizationSystem localization,
						   IEnergyProvider energyProvider)
		{
			LoadNextLevelCommand = loadNextLevelCommand;
			LocalizationSystem = localization;
			Pack = pack;
			SavedPackData = savedPackData;
			EnergyProvider = energyProvider;
		}

		public IEnergyProvider EnergyProvider { get; }
		public ILabeledCommand LoadNextLevelCommand { get; }
		public ILocalizationSystem LocalizationSystem { get; }
		public Pack Pack { get; }
		public SavedPackData SavedPackData { get; }
	}
}
