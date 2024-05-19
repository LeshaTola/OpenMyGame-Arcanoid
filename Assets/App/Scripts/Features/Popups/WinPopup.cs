using Features.StateMachine;
using Features.StateMachine.States;
using Module.PopupLogic.General;
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

		public bool IsActive { get; private set; }

		[Inject]
		public void Construct(StateMachineHandler stateMachineHandler)
		{
			this.stateMachineHandler = stateMachineHandler;
		}

		public void Init()
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
			popupAnimation.Value.Show(() =>
			{
				Activate();
				IsActive = true;
			});

		}

		private void NextButtonClicked()
		{
			stateMachineHandler.Core.ChangeState<LoadSceneState>();
		}
	}
}