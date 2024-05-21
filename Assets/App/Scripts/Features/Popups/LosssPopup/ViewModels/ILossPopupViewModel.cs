using Module.Commands;

namespace Features.Popups.LossPopup.ViewModels
{
	public interface ILossPopupViewModel
	{
		public ILabeledCommand RestartCommand { get; }
	}
}