using Features.Popups.Animations.Animator;
using Module.Commands;
using Module.Localization;
using System.Collections.Generic;

namespace Features.Popups.Languages
{
	public interface IGeneralPopupViewModel
	{
		public string Header { get; }
		public IEnumerable<ILabeledCommand> Commands { get; }
		public ILocalizationSystem LocalizationSystem { get; }
		public IButtonsFactory ButtonsFactory { get; }
		public IPopupAnimator PopupAnimator { get; }
	}
}