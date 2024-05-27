using Features.Energy.Providers;
using Features.Saves;
using Module.Commands;
using Module.Localization;
using Scenes.PackSelection.Feature.Packs.Configs;

namespace Features.Popups.WinPopup.ViewModels
{
	public interface IWinPopupViewModel
	{
		ILabeledCommand LoadNextLevelCommand { get; }
		Pack Pack { get; }
		public SavedPackData SavedPackData { get; }
		ILocalizationSystem LocalizationSystem { get; }
		IEnergyProvider EnergyProvider { get; }
	}
}
