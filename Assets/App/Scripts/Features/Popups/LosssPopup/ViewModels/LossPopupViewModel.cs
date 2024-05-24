using Module.Commands;
using Module.Localization;

namespace Features.Popups.Loss.ViewModels
{
	public class LossPopupViewModel : ILossPopupViewModel
	{
		public ILabeledCommand RestartCommand { get; }
		public ILocalizationSystem LocalizationSystem { get; }

		public LossPopupViewModel(ILabeledCommand executeRestart, ILocalizationSystem localizationSystem)
		{
			RestartCommand = executeRestart;
			LocalizationSystem = localizationSystem;
		}
	}
}