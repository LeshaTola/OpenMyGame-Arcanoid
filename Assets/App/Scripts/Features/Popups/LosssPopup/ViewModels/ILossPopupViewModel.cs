using Features.Energy.Providers;
using Features.Popups.Animations.Animator;
using Module.Commands;
using Module.Localization;

namespace Features.Popups.Loss.ViewModels
{
	public interface ILossPopupViewModel
	{
		public ILabeledCommand RestartCommand { get; }
		public ILabeledCommand ContinueCommand { get; }
		public ILabeledCommand BackCommand { get; }
		public ILocalizationSystem LocalizationSystem { get; }
		public IPopupAnimator PopupAnimator { get; }
		public IEnergyProvider EnergyProvider { get; }

	}
}