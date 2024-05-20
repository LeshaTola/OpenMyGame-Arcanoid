using Features.StateMachine;
using Scenes.PackSelection.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scenes.PackSelection.Feature.UI
{
	public class HeaderUI : MonoBehaviour, Features.Bootstrap.IInitializable
	{
		[SerializeField] private Button exitButton;

		private StateMachineHandler stateMachineHandler;

		[Inject]
		public void Construct(StateMachineHandler stateMachineHandler)
		{
			this.stateMachineHandler = stateMachineHandler;
		}

		public void Init()
		{
			exitButton.onClick.AddListener(ExitButtonClicked);
		}

		public void ExitButtonClicked()
		{
			stateMachineHandler.Core.ChangeState<LoadMainMenuState>();
		}
	}
}
