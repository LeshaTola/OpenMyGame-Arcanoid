using Cysharp.Threading.Tasks;

namespace Features.Popups.WinPopup.Animator
{
	public interface IWinPopupAnimator
	{
		UniTask AnimateUI(WinAnimationData winAnimationData);
	}
}