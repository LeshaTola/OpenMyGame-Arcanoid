using DG.Tweening;
using Features.StateMachine;
using Features.StateMachine.States;
using Features.UI.Animations.SpinAnimation;
using Module.PopupLogic.General;
using Module.PopupLogic.General.Popup;
using Scenes.Gameplay.StateMachine.States;
using Scenes.PackSelection.Feature.Packs;
using Scenes.PackSelection.Feature.Packs.Configs;
using TMPro;
using TNRD;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.Popups
{
	public class WinPopup : MonoBehaviour, IPopup
	{
		[SerializeField] private SerializableInterface<IPopupAnimation> popupAnimation;
		[SerializeField] private Image packImage;
		[SerializeField] private Button nextButton;
		[SerializeField] private TextMeshProUGUI packName;
		[SerializeField] private TextMeshProUGUI levelInfo;
		[SerializeField] private SpinAnimation lines;
		[SerializeField] private const float eachAnimationDuration = 0.5f;

		private StateMachineHandler stateMachineHandler;
		private IPackProvider packProvider;

		public bool IsActive { get; private set; }

		[Inject]
		public void Construct(StateMachineHandler stateMachineHandler, IPackProvider packProvider)
		{
			this.stateMachineHandler = stateMachineHandler;
			this.packProvider = packProvider;
		}

		public void Setup()
		{
			nextButton.onClick.AddListener(NextButtonClicked);
			Hide();
		}

		public void Activate()
		{
			nextButton.enabled = true;
		}

		public void Deactivate()
		{
			nextButton.enabled = false;
		}

		public void Hide()
		{
			ResetUI();
			IsActive = false;
			popupAnimation.Value.Hide(() =>
			{
				Deactivate();
				gameObject.SetActive(false);
			});
		}

		public void Show()
		{
			gameObject.SetActive(true);
			lines.StartAnimation();
			popupAnimation.Value.Show(() =>
			{
				UpdateUI();
				IsActive = true;
			});

		}

		private void UpdateUI()
		{
			Pack pack = packProvider.CurrentPack;
			if (pack == null)
			{
				return;
			}

			Sequence sequence = DOTween.Sequence();
			SetupLevelAnimation(pack, sequence);
			SetupButtonAnimation(sequence);
			sequence.onComplete += Activate;
		}

		private void ResetUI()
		{
			levelInfo.text = "0/0";
			packName.text = "";
			nextButton.transform.localScale = Vector3.zero;
		}

		private void NextButtonClicked()
		{
			Pack currentPack = packProvider.CurrentPack;
			if (currentPack == null || currentPack.CurrentLevel == currentPack.MaxLevel)
			{
				stateMachineHandler.Core.ChangeState<LoadSceneState>();
				return;
			}

			currentPack.CurrentLevel++;
			stateMachineHandler.Core.ChangeState<InitialState>();
		}

		private void SetupLevelAnimation(Pack pack, Sequence sequence)
		{
			var maxLevelAnimation = DOVirtual.Int(0, pack.MaxLevel + 1, eachAnimationDuration, value =>
			{
				levelInfo.text = $"0/{value}";
			});

			var levelAnimation = DOVirtual.Int(0, pack.CurrentLevel + 1, eachAnimationDuration, value =>
			{
				levelInfo.text = $"{value}/{pack.MaxLevel + 1}";
			});

			levelAnimation.onComplete += () =>
			{
				packName.text = pack.Name;
				packImage.sprite = pack.Sprite;
			};
			sequence.Append(maxLevelAnimation);
			sequence.Append(levelAnimation);
		}

		private void SetupButtonAnimation(Sequence sequence)
		{
			sequence.Append(nextButton.transform.DOScale(Vector3.one, eachAnimationDuration));
		}
	}
}