using Module.Commands;

namespace Features.Popups.Menu.ViewModels
{
	public interface IMenuPopupViewModel
	{
		public ILabeledCommand RestartCommand { get; }
		public ILabeledCommand BackCommand { get; }
		public ILabeledCommand ResumeCommand { get; }
	}
}