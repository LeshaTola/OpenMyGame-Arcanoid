using Module.Commands;

namespace Features.Popups.Loss.ViewModels
{
	public class LossPopupViewModel : ILossPopupViewModel
	{
		public ILabeledCommand RestartCommand { get; }

		public LossPopupViewModel(ILabeledCommand executeRestart)
		{
			RestartCommand = executeRestart;
		}
	}
}