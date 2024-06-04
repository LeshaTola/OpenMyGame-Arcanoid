using Cysharp.Threading.Tasks;
using DG.Tweening;
using Features.Energy;
using Features.Energy.UI;
using Features.Popups.Languages;
using Features.Popups.WinPopup.ViewModels;
using Features.Saves;
using Features.UI.Animations.SpinAnimation;
using Module.Localization.Localizers;
using Module.PopupLogic.General.Popups;
using Scenes.PackSelection.Feature.Packs.Configs;
using Sirenix.OdinInspector;
using TMPro;
using TNRD;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Popups.WinPopup
{
	public class WinPopup : Popup
	{
		[SerializeField] private SerializableInterface<IEnergySliderUI> energySlider;
		[SerializeField] private PopupButton nextButton;

		[FoldoutGroup("Pack Image")]
		[SerializeField] private SpinAnimation lines;
		[FoldoutGroup("Pack Image")]
		[SerializeField] private RectTransform packImageContainer;
		[FoldoutGroup("Pack Image")]
		[SerializeField] private Image packImage;

		[FoldoutGroup("Text")]
		[SerializeField] private TMProLocalizer header;
		[FoldoutGroup("Text")]
		[SerializeField] private TMProLocalizer packPreNameText;
		[FoldoutGroup("Text")]
		[SerializeField] private TMProLocalizer packName;

		[FoldoutGroup("Text/Label")]
		[SerializeField] private RectTransform levelLabel;
		[FoldoutGroup("Text/Label")]
		[SerializeField] private TextMeshProUGUI levelInfo;


		[FoldoutGroup("Animations")]
		[SerializeField] private float energyAnimationDuration = 1f;
		[FoldoutGroup("Animations")]
		[SerializeField] private float imageAnimationDuration = 0.5f;
		[FoldoutGroup("Animations")]
		[SerializeField] private float levelAnimationDuration = 0.25f;
		[FoldoutGroup("Animations")]
		[SerializeField] private float buttonAnimationDuration = 0.5f;

		private IWinPopupViewModel viewModel;
		private EnergyController energyController;

		public void Setup(IWinPopupViewModel viewModel)
		{
			CleanUp();
			Initialize(viewModel);
			SetupLogic(viewModel);
			Translate();

			lines.StartAnimation();
			SetStartEnergyValue();

		}

		public async override UniTask Show()
		{
			gameObject.SetActive(true);
			Controller.AddActivePopup(this);

			await popupAnimation.Value.Show();
			await AnimateUI();
			Activate();
		}

		public async UniTask AnimateUI()
		{
			Sequence sequence = DOTween.Sequence();

			SetupHeaderAnimation(sequence);
			SetupSliderAnimation(sequence);
			SetupImageAnimation(viewModel.Pack, sequence);
			SetupLevelAnimation(viewModel.Pack, viewModel.SavedPackData, sequence);
			SetupButtonAnimation(sequence);

			await sequence.AsyncWaitForCompletion();
		}

		#region SetupAnimation

		private void SetStartEnergyValue()
		{
			energySlider.Value.UpdateUI(viewModel.EnergyProvider.CurrentEnergy - viewModel.EnergyProvider.Config.WinReward, viewModel.EnergyProvider.Config.MaxEnergy);
		}

		private void SetupHeaderAnimation(Sequence sequence)
		{
			sequence.Append(header.transform.DOScale(Vector3.one, levelAnimationDuration).SetEase(Ease.OutBack));
		}

		private void SetupImageAnimation(Pack pack, Sequence sequence)
		{
			sequence.Append(packImageContainer
				.DOScale(Vector3.one, imageAnimationDuration)
				.SetEase(Ease.OutBack));

			if (pack == null || packImage.sprite == pack.Sprite)
			{
				return;
			}
			Tween halfOfRotation = packImage.transform.DORotate(new Vector3(0, 90f, 0), imageAnimationDuration / 2);
			halfOfRotation.onComplete += () => packImage.sprite = pack.Sprite;
			Tween secondHalfOfRotation = packImage.transform.DORotate(new Vector3(0, 0, 0), imageAnimationDuration / 2);

			secondHalfOfRotation.onComplete += () =>
			{
				packName.Key = pack.Name;
				packName.Translate();
			};

			sequence.Append(halfOfRotation);
			sequence.Append(secondHalfOfRotation);
		}

		private void SetupSliderAnimation(Sequence sequence)
		{
			int startValue = viewModel.EnergyProvider.CurrentEnergy - viewModel.EnergyProvider.Config.WinReward;
			var sliderAnimation =
				DOVirtual.Int(
					startValue,
					viewModel.EnergyProvider.CurrentEnergy,
					energyAnimationDuration,
					value =>
			{
				energySlider.Value.UpdateUI(value, viewModel.EnergyProvider.Config.MaxEnergy);
			});

			sliderAnimation.onComplete += () =>
			{
				energyController = new EnergyController(energySlider.Value, viewModel.EnergyProvider);
			};

			sequence.Append(sliderAnimation);
		}

		private void SetupLevelAnimation(Pack pack, SavedPackData savedPackData, Sequence sequence)
		{
			sequence.Append(levelLabel
				.DOLocalMove(
				new Vector2(
					levelLabel.localPosition.x,
					levelLabel.localPosition.y - levelLabel.rect.height),
				imageAnimationDuration).SetEase(Ease.OutBounce));
			if (pack == null || savedPackData == null)
			{
				return;
			}
			SetupMaxLevelAnimation(pack, sequence);
			SetupCurrentLevelAnimation(pack, savedPackData, sequence);
		}

		private void SetupCurrentLevelAnimation(Pack pack, SavedPackData savedPackData, Sequence sequence)
		{
			var levelAnimation = DOVirtual.Int(0, savedPackData.CurrentLevel, levelAnimationDuration, value =>
			{
				levelInfo.text = $"{value}/{pack.MaxLevel + 1}";
			});
			sequence.Append(levelAnimation);
		}

		private void SetupMaxLevelAnimation(Pack pack, Sequence sequence)
		{
			var maxLevelAnimation = DOVirtual.Int(0, pack.MaxLevel + 1, levelAnimationDuration, value =>
			{
				levelInfo.text = $"0/{value}";
			});
			sequence.Append(maxLevelAnimation);
		}

		private void SetupButtonAnimation(Sequence sequence)
		{
			sequence.Append(nextButton.transform.DOScale(Vector3.one, buttonAnimationDuration).SetEase(Ease.OutBack));
		}
		#endregion

		private void Initialize(IWinPopupViewModel viewModel)
		{
			this.viewModel = viewModel;

			header.Init(viewModel.LocalizationSystem);
			packName.Init(viewModel.LocalizationSystem);
			packPreNameText.Init(viewModel.LocalizationSystem);

			nextButton.Init(viewModel.LocalizationSystem);
		}

		private void SetupLogic(IWinPopupViewModel viewModel)
		{
			nextButton.onButtonClicked += viewModel.LoadNextLevelCommand.Execute;
			nextButton.UpdateText(viewModel.LoadNextLevelCommand.Label);
		}

		private void Translate()
		{
			header.Translate();
			packPreNameText.Translate();

			nextButton.Translate();
		}

		private void CleanUp()
		{
			ResetUI();

			if (viewModel != null)
			{
				nextButton.onButtonClicked -= viewModel.LoadNextLevelCommand.Execute;
				energyController.CleanUp();
			}
		}

		private void ResetUI()
		{
			levelInfo.text = "0/0";
			packName.Text.text = "";

			header.transform.localScale = Vector3.zero;
			nextButton.transform.localScale = Vector3.zero;
			packImageContainer.localScale = Vector3.zero;
			levelLabel.localPosition = new Vector2(
				levelLabel.localPosition.x,
				levelLabel.localPosition.y + levelLabel.rect.height);
		}
	}
}