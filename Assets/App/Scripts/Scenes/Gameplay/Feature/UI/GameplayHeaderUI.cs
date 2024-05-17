using Features.Bootstrap;
using Features.StateMachine;
using Features.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Gameplay.Feature.UI
{
	public class GameplayHeaderUI : MonoBehaviour, IInitializable
	{
		[SerializeField] private StateMachineHandler stateMachine;
		[SerializeField] private Button mainMenuButton;

		public void Init()
		{
			mainMenuButton.onClick.AddListener(() => { stateMachine.Core.ChangeState<LoadSceneState>(); });
		}
	}
}