using Module.Commands;

namespace Features.Popups.Menu.ViewModels
{
	public class MenuPopupViewModel : IMenuPopupViewModel
	{
		public MenuPopupViewModel(ILabeledCommand restartCommand, ILabeledCommand backCommand, ILabeledCommand resumeCommand)
		{
			RestartCommand = restartCommand;
			BackCommand = backCommand;
			ResumeCommand = resumeCommand;
		}

		public ILabeledCommand RestartCommand { get; }
		public ILabeledCommand BackCommand { get; }
		public ILabeledCommand ResumeCommand { get; }
	}
}