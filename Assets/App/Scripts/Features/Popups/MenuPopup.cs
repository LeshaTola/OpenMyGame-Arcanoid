using Features.StateMachine;
using Features.StateMachine.States;
using Module.PopupLogic.General;
using Scenes.Gameplay.StateMachine.States;
using TNRD;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.Popups
{
	public class MenuPopup : MonoBehaviour, IPopup
	{
		[SerializeField] private SerializableInterface<IPopupAnimation> popupAnimation;
		[SerializeField] private Button restartButton;
		[SerializeField] private Button backButton;
		[SerializeField] private Button resumeButton;

		private StateMachineHandler stateMachineHandler;
		private IPopupController popupController;//TODO Move into pause state

		public bool IsActive { get; private set; }

		[Inject]
		public void Construct(StateMachineHandler stateMachineHandler, IPopupController popupController)
		{
			this.stateMachineHandler = stateMachineHandler;
			this.popupController = popupController;
		}

		public void Init()
		{
			restartButton.onClick.AddListener(OnRestartButtonClicked);
			backButton.onClick.AddListener(OnBackButtonClicked);
			resumeButton.onClick.AddListener(OnResumeButtonClicked);

			Hide();
		}

		public void Activate()
		{
			restartButton.enabled = true;
			backButton.enabled = true;
			resumeButton.enabled = true;
		}

		public void Deactivate()
		{
			restartButton.enabled = false;
			backButton.enabled = false;
			resumeButton.enabled = false;
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

		private void OnRestartButtonClicked()
		{
			stateMachineHandler.Core.ChangeState<InitialState>();
			popupController.HidePopup();
		}

		private void OnBackButtonClicked()
		{
			stateMachineHandler.Core.ChangeState<LoadSceneState>();
			popupController.HidePopup();
		}

		private void OnResumeButtonClicked()
		{
			popupController.HidePopup();
		}
	}
}