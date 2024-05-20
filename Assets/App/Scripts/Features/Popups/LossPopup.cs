using Features.StateMachine;
using Module.PopupLogic.General;
using Scenes.Gameplay.StateMachine.States;
using TNRD;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.Popups
{
	public class LossPopup : MonoBehaviour, IPopup
	{
		[SerializeField] private SerializableInterface<IPopupAnimation> popupAnimation;
		[SerializeField] private Button restartButton;

		private StateMachineHandler stateMachineHandler;

		public bool IsActive { get; private set; }

		[Inject]
		public void Construct(StateMachineHandler stateMachineHandler)
		{
			this.stateMachineHandler = stateMachineHandler;
		}

		public void Init()
		{
			restartButton.onClick.AddListener(RestartButtonClicked);
			Hide();
		}

		public void Activate()
		{
			restartButton.enabled = true;
		}

		public void Deactivate()
		{
			restartButton.enabled = false;
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

		private void RestartButtonClicked()
		{
			stateMachineHandler.Core.ChangeState<InitialState>();
		}
	}
}