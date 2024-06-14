using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Features.Popups.WinPopup.Animator
{
	public class WinPopupAnimator : IWinPopupAnimator
	{
		private WinAnimationData winAnimationData;

		public async UniTask AnimateUI(WinAnimationData winAnimationData)
		{
			this.winAnimationData = winAnimationData;

			Sequence sequence = DOTween.Sequence();

			SetupHeaderAnimation(sequence);
			SetupSliderAnimation(sequence);
			SetupImageAnimation(sequence);
			SetupLevelAnimation(sequence);
			SetupButtonAnimation(sequence);
			await sequence.AsyncWaitForCompletion();
		}


		#region SetupAnimation

		private void SetupHeaderAnimation(Sequence sequence)
		{
			sequence.Append(winAnimationData.Header.transform.DOScale(Vector3.one, winAnimationData.levelAnimationDuration).SetEase(Ease.OutBack));
		}

		private void SetupImageAnimation(Sequence sequence)
		{
			sequence.Append(winAnimationData.PackImageContainer
				.DOScale(Vector3.one, winAnimationData.imageAnimationDuration)
				.SetEase(Ease.OutBack));

			if (winAnimationData.targetSprite != null && winAnimationData.PackImage.sprite == winAnimationData.targetSprite)
			{
				SetPackName();
				return;
			}

			Tween halfOfRotation = winAnimationData.PackImage.transform.DORotate(new Vector3(0, 90f, 0), winAnimationData.imageAnimationDuration / 2);
			halfOfRotation.onComplete += () => winAnimationData.PackImage.sprite = winAnimationData.targetSprite;
			Tween secondHalfOfRotation = winAnimationData.PackImage.transform.DORotate(new Vector3(0, 0, 0), winAnimationData.imageAnimationDuration / 2);

			secondHalfOfRotation.onComplete += () =>
			{
				SetPackName();
			};
			sequence.Append(halfOfRotation);
			sequence.Append(secondHalfOfRotation);
		}

		private void SetPackName()
		{
			winAnimationData.PackName.Key = winAnimationData.targetPackName;
			winAnimationData.PackName.Translate();
		}

		private void SetupSliderAnimation(Sequence sequence)
		{
			var sliderAnimation =
				DOVirtual.Int(
					winAnimationData.startEnergy,
					winAnimationData.targetEnergy,
					winAnimationData.energyAnimationDuration,
					value =>
					{
						winAnimationData.EnergySlider.Value.UpdateUI(value, winAnimationData.maxEnergy);
					});

			sequence.Append(sliderAnimation);
		}

		private void SetupLevelAnimation(Sequence sequence)
		{
			sequence.Append(winAnimationData.LevelLabel
				.DOLocalMove(
				new Vector2(
					winAnimationData.LevelLabel.localPosition.x,
					winAnimationData.LevelLabel.localPosition.y - winAnimationData.LevelLabel.rect.height),
				winAnimationData.imageAnimationDuration).SetEase(Ease.OutBounce));

			SetupCurrentLevelAnimation(sequence);
		}

		private void SetupCurrentLevelAnimation(Sequence sequence)
		{
			var levelAnimation = DOVirtual.Int(winAnimationData.targetLevel, winAnimationData.targetLevel, winAnimationData.levelAnimationDuration, value =>
			{
				winAnimationData.LevelInfo.text = $"{value}/{winAnimationData.maxLevel}";
			});
			sequence.Append(levelAnimation);
		}

		private void SetupButtonAnimation(Sequence sequence)
		{
			sequence.Append(winAnimationData.NextButton.transform.DOScale(Vector3.one, winAnimationData.buttonAnimationDuration).SetEase(Ease.OutBack));
		}
		#endregion

	}
}
