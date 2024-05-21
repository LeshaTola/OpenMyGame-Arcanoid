using Module.Commands;

namespace Features.Popups.Loss.ViewModels
{
	public interface ILossPopupViewModel
	{
		public ILabeledCommand RestartCommand { get; }
	}
}