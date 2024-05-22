using DG.Tweening;
using Features.Popups.WinPopup.ViewModels;
using Features.UI.Animations.SpinAnimation;
using Module.PopupLogic.General.Popups;
using Scenes.PackSelection.Feature.Packs.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Popups.WinPopup
{
	public class WinPopup : Popup
	{
		[SerializeField] private Image packImage;

		[SerializeField] private Button nextButton;
		[SerializeField] private TextMeshProUGUI nextButtonText;

		[SerializeField] private TextMeshProUGUI packName;
		[SerializeField] private TextMeshProUGUI levelInfo;

		[SerializeField] private SpinAnimation lines;
		[SerializeField] private float eachAnimationDuration = 0.5f;

		private IWinPopupViewModel viewModel;

		public void Setup(IWinPopupViewModel viewModel)
		{
			CleanUp();
			nextButton.onClick.AddListener(viewModel.LoadNextLevelCommand.Execute);
			nextButtonText.text = viewModel.LoadNextLevelCommand.Label;

			this.viewModel = viewModel;
		}

		public override void Show()
		{
			gameObject.SetActive(true);
			lines.StartAnimation();
			popupAnimation.Value.Show(() =>
			{
				Controller.AddActivePopup(this);
				AnimateUI();
			});
		}

		public void AnimateUI()
		{
			var pack = viewModel.Pack;
			if (pack == null)
			{
				return;
			}

			Sequence sequence = DOTween.Sequence();
			SetupLevelAnimation(pack, sequence);
			SetupButtonAnimation(sequence);
			sequence.onComplete += Activate;
		}

		private void CleanUp()
		{
			nextButton.onClick.RemoveAllListeners();
			ResetUI();
		}

		private void ResetUI()
		{
			levelInfo.text = "0/0";
			packName.text = "";
			nextButton.transform.localScale = Vector3.zero;
		}

		private void SetupLevelAnimation(Pack pack, Sequence sequence)
		{
			SetupMaxLevelAnimation(pack, sequence);
			SetupCurrentLevelAnimation(pack, sequence);
		}

		private void SetupCurrentLevelAnimation(Pack pack, Sequence sequence)
		{
			var levelAnimation = DOVirtual.Int(0, pack.CurrentLevel + 1, eachAnimationDuration, value =>
			{
				levelInfo.text = $"{value}/{pack.MaxLevel + 1}";
			});

			levelAnimation.onComplete += () =>
			{
				packName.text = pack.Name;
				packImage.sprite = pack.Sprite;
			};
			sequence.Append(levelAnimation);
		}

		private void SetupMaxLevelAnimation(Pack pack, Sequence sequence)
		{
			var maxLevelAnimation = DOVirtual.Int(0, pack.MaxLevel + 1, eachAnimationDuration, value =>
			{
				levelInfo.text = $"0/{value}";
			});
			sequence.Append(maxLevelAnimation);
		}

		private void SetupButtonAnimation(Sequence sequence)
		{
			sequence.Append(nextButton.transform.DOScale(Vector3.one, eachAnimationDuration));
		}
	}
}