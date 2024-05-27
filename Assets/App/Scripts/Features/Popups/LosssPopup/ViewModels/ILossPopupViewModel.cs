using Module.Commands;
using Module.Localization;

namespace Features.Popups.Loss.ViewModels
{
	public interface ILossPopupViewModel
	{
		public ILabeledCommand RestartCommand { get; }
		public ILabeledCommand ContinueCommand { get; }
		public ILabeledCommand BackCommand { get; }
		ILocalizationSystem LocalizationSystem { get; }
	}
}