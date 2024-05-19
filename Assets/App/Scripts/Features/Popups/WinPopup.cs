using Features.StateMachine;
using Features.StateMachine.States;
using Module.PopupLogic.General;
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

		private StateMachineHandler stateMachineHandler;
		private IPackProvider packProvider;

		public bool IsActive { get; private set; }

		[Inject]
		public void Construct(StateMachineHandler stateMachineHandler, IPackProvider packProvider)
		{
			this.stateMachineHandler = stateMachineHandler;
			this.packProvider = packProvider;
		}

		public void Init()
		{
			nextButton.onClick.AddListener(NextButtonClicked);
			Hide();
		}

		public void UpdateUI()
		{
			Pack pack = packProvider.CurrentPack;
			if (pack == null)
			{
				return;
			}

			packName.text = pack.Name;
			packImage.sprite = pack.Sprite;
			levelInfo.text = $"{pack.CurrentLevel + 1}/{pack.MaxLevel + 1}";
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
			IsActive = false;
			popupAnimation.Value.Hide(() =>
			{
				Deactivate();
				gameObject.SetActive(false);
			});
		}

		public void Show()
		{
			UpdateUI();
			gameObject.SetActive(true);
			popupAnimation.Value.Show(() =>
			{
				Activate();
				IsActive = true;
			});

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
	}
}