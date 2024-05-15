using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI.Animations.SwapColor
{
	public class StandardSwapColorAnimation : MonoBehaviour, ISwapColorAnimation
	{
		[SerializeField] private Image image;
		[SerializeField] private Color showColor = Color.white;
		[SerializeField] private Color hideColor = Color.white;
		[SerializeField] private float animationTime;

		private Tween tween;

		public void Show()
		{
			CleanUp();
			tween = image.DOColor(showColor, animationTime);
		}

		public void Hide()
		{
			CleanUp();
			tween = image.DOColor(hideColor, animationTime);
		}

		private void CleanUp()
		{
			if (tween != null)
			{
				tween.Kill();
			}
		}
	}


}