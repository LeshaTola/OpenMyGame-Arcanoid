using Features.StateMachine;
using Module.PopupLogic.General;
using Scenes.Gameplay.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scenes.Gameplay.Feature.UI
{
	public class GameplayHeaderUI : MonoBehaviour, Features.Bootstrap.IInitializable
	{
		[SerializeField] private Button mainMenuButton;

		private IPopupController popupController;
		private StateMachineHandler stateMachineHandler;

		[Inject]
		public void Construct(IPopupController popupController, StateMachineHandler stateMachineHandler)
		{
			this.popupController = popupController;
			this.stateMachineHandler = stateMachineHandler;

			popupController.OnFirstPopupActivates += OnFirstPopupActivates;
			popupController.OnLastPopupDeactivates += OnLastPopupDeactivates;
		}

		private void OnDestroy()
		{
			popupController.OnFirstPopupActivates -= OnFirstPopupActivates;
			popupController.OnLastPopupDeactivates -= OnLastPopupDeactivates;
		}

		public void Init()
		{
			mainMenuButton.onClick.AddListener(MenuButtonClicked);
		}

		public void MenuButtonClicked()
		{
			stateMachineHandler.Core.ChangeState<PauseState>();
		}

		private void Activate()
		{
			mainMenuButton.enabled = true;
		}

		private void Deactivate()
		{
			mainMenuButton.enabled = false;
		}

		private void OnLastPopupDeactivates()
		{
			Activate();
		}

		private void OnFirstPopupActivates()
		{
			Deactivate();
		}
	}
}