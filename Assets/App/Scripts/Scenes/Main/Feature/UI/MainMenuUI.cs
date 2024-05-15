using Features.Bootstrap;
using Features.StateMachine;
using Features.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Main.Feature.UI
{
	public class MainMenuUI : MonoBehaviour, IInitializable
	{
		[SerializeField] private MonoBehStateMachine stateMachine;
		[SerializeField] private Button playButton;

		public void Init()
		{
			playButton.onClick.AddListener(() => stateMachine.Core.ChangeState<LoadSceneState>());
		}
	}
}