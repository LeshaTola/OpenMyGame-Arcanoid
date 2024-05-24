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
						   ILocalizationSystem localization)
		{
			LoadNextLevelCommand = loadNextLevelCommand;
			LocalizationSystem = localization;
			Pack = pack;
			SavedPackData = savedPackData;
		}

		public ILabeledCommand LoadNextLevelCommand { get; }
		public ILocalizationSystem LocalizationSystem { get; }
		public Pack Pack { get; }
		public SavedPackData SavedPackData { get; }

	}
}
