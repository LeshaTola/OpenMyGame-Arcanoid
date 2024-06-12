using Cysharp.Threading.Tasks;
using Features.Energy.Controllers;
using Features.Energy.UI;
using Features.Popups.Languages;
using Features.Popups.WinPopup.Animator;
using Features.Popups.WinPopup.ViewModels;
using Features.UI.Animations.SpinAnimation;
using Module.Localization.Localizers;
using Module.PopupLogic.General.Popups;
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

			ResetUI();

			lines.StartAnimation();
		}

		public async override UniTask Show()
		{
			gameObject.SetActive(true);
			Controller.AddActivePopup(this);

			await popupAnimation.Value.Show();

			WinAnimationData winAnimationData = GetWinAnimationData();

			await viewModel.WinPopupAnimator.AnimateUI(winAnimationData);
			energyController = new EnergyController(energySlider.Value, viewModel.EnergyProvider);

			Activate();
		}

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
			if (viewModel != null)
			{
				nextButton.onButtonClicked -= viewModel.LoadNextLevelCommand.Execute;
				energyController.CleanUp();
			}
		}

		private void ResetUI()
		{
			if (viewModel.Pack == null || viewModel.SavedPackData == null)
			{
				levelInfo.text = $"0/{viewModel.Pack.MaxLevel + 1}";
			}
			else
			{
				levelInfo.text = $"0/0";
			}

			energySlider.Value.UpdateUI(
				viewModel.EnergyProvider.CurrentEnergy - viewModel.EnergyProvider.Config.WinReward,
				viewModel.EnergyProvider.Config.MaxEnergy);

			packName.Text.text = "";

			header.transform.localScale = Vector3.zero;
			nextButton.transform.localScale = Vector3.zero;
			packImageContainer.localScale = Vector3.zero;
			levelLabel.localPosition = new Vector2(
				levelLabel.localPosition.x,
				levelLabel.localPosition.y + levelLabel.rect.height);
		}

		private WinAnimationData GetWinAnimationData()
		{
			return new WinAnimationData()
			{
				buttonAnimationDuration = buttonAnimationDuration,
				energyAnimationDuration = energyAnimationDuration,
				imageAnimationDuration = imageAnimationDuration,
				levelAnimationDuration = levelAnimationDuration,

				EnergySlider = energySlider,
				NextButton = nextButton,
				Lines = lines,
				PackImageContainer = packImageContainer,
				PackImage = packImage,
				Header = header,
				PackPreNameText = packPreNameText,
				PackName = packName,
				LevelLabel = levelLabel,
				LevelInfo = levelInfo,

				targetEnergy = viewModel.EnergyProvider.CurrentEnergy,
				startEnergy = viewModel.EnergyProvider.CurrentEnergy - viewModel.EnergyProvider.Config.WinReward,
				maxEnergy = viewModel.EnergyProvider.Config.MaxEnergy,

				targetLevel = viewModel.SavedPackData == null ? 1 : viewModel.SavedPackData.CurrentLevel,
				maxLevel = viewModel.Pack == null ? 1 : viewModel.Pack.MaxLevel + 1,

				targetPackName = viewModel.Pack == null ? "Test" : viewModel.Pack.Name,
				targetSprite = viewModel.Pack == null ? null : viewModel.Pack.Sprite,
			};
		}
	}
}