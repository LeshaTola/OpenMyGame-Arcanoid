using Cysharp.Threading.Tasks;
using Features.Popups.Languages;
using Module.Localization.Localizers;
using System.Collections.Generic;

namespace Features.Popups.Animations.Animator
{
	public interface IPopupAnimator
	{
		void Setup(TMProLocalizer header, List<PopupButton> buttons, float eachAnimationDuration);
		UniTask ShowAnimation();
		UniTask HideAnimation();
		void ResetAnimation();
	}
}
