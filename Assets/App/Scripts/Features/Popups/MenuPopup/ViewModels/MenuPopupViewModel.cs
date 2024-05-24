using Module.Commands;
using Module.Localization;

namespace Features.Popups.Menu.ViewModels
{
	public class MenuPopupViewModel : IMenuPopupViewModel
	{
		public MenuPopupViewModel(ILabeledCommand restartCommand,
							ILabeledCommand backCommand,
							ILabeledCommand resumeCommand,
							ILocalizationSystem localizationSystem)
		{
			RestartCommand = restartCommand;
			BackCommand = backCommand;
			ResumeCommand = resumeCommand;
			LocalizationSystem = localizationSystem;

		}

		public ILabeledCommand RestartCommand { get; }
		public ILabeledCommand BackCommand { get; }
		public ILabeledCommand ResumeCommand { get; }
		public ILocalizationSystem LocalizationSystem { get; }
	}
}