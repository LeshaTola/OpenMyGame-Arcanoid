using Module.Commands;
using Module.Localization;

namespace Features.Popups.Loss.ViewModels
{
	public interface ILossPopupViewModel
	{
		public ILabeledCommand RestartCommand { get; }
		ILocalizationSystem LocalizationSystem { get; }
	}
}