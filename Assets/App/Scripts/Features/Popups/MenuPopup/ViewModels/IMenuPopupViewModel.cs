using Features.Popups.Animations.Animator;
using Module.Commands;
using Module.Localization;

namespace Features.Popups.Menu.ViewModels
{
	public interface IMenuPopupViewModel
	{
		ILabeledCommand RestartCommand { get; }
		ILabeledCommand BackCommand { get; }
		ILabeledCommand ResumeCommand { get; }
		ILocalizationSystem LocalizationSystem { get; }
		IPopupAnimator PopupAnimator { get; }
	}
}