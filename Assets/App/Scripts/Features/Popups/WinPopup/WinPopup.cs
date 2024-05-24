using DG.Tweening;
using Features.Popups.Languages;
using Features.Popups.WinPopup.ViewModels;
using Features.Saves;
using Features.UI.Animations.SpinAnimation;
using Module.Localization.Localizers;
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

		[SerializeField] private PopupButton nextButton;

		[SerializeField] private TMProLocalizer header;
		[SerializeField] private TMProLocalizer packPreNameText;
		[SerializeField] private TMProLocalizer packName;

		[SerializeField] private TextMeshProUGUI levelInfo;

		[SerializeField] private SpinAnimation lines;
		[SerializeField] private float eachAnimationDuration = 0.5f;

		private IWinPopupViewModel viewModel;

		public void Setup(IWinPopupViewModel viewModel)
		{
			CleanUp();
			Initialize(viewModel);
			SetupLogic(viewModel);
			Translate();

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
			var savedPackData = viewModel.SavedPackData;
			if (pack == null || savedPackData == null)
			{
				return;
			}

			Sequence sequence = DOTween.Sequence();
			SetupLevelAnimation(pack, savedPackData, sequence);
			SetupButtonAnimation(sequence);
			sequence.onComplete += Activate;
		}

		private void SetupLevelAnimation(Pack pack, SavedPackData savedPackData, Sequence sequence)
		{
			SetupMaxLevelAnimation(pack, sequence);
			SetupCurrentLevelAnimation(pack, savedPackData, sequence);
		}

		private void SetupCurrentLevelAnimation(Pack pack, SavedPackData savedPackData, Sequence sequence)
		{
			var levelAnimation = DOVirtual.Int(0, savedPackData.CurrentLevel, eachAnimationDuration, value =>
			{
				levelInfo.text = $"{value}/{pack.MaxLevel + 1}";
			});

			levelAnimation.onComplete += () =>
			{
				packName.Key = pack.Name;
				packName.Translate();
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

		private void SetupLogic(IWinPopupViewModel viewModel)
		{
			nextButton.onButtonClicked += viewModel.LoadNextLevelCommand.Execute;
			nextButton.UpdateText(viewModel.LoadNextLevelCommand.Label);
		}

		private void CleanUp()
		{
			ResetUI();

			if (viewModel != null)
			{
				nextButton.onButtonClicked -= viewModel.LoadNextLevelCommand.Execute;
			}
		}

		private void ResetUI()
		{
			levelInfo.text = "0/0";
			packName.Text = "";
			nextButton.transform.localScale = Vector3.zero;
		}

		private void Translate()
		{
			header.Translate();
			packPreNameText.Translate();

			nextButton.Translate();
		}

		private void Initialize(IWinPopupViewModel viewModel)
		{
			this.viewModel = viewModel;

			header.Init(viewModel.LocalizationSystem);
			packName.Init(viewModel.LocalizationSystem);
			packPreNameText.Init(viewModel.LocalizationSystem);


			nextButton.Init(viewModel.LocalizationSystem);
		}
	}
}