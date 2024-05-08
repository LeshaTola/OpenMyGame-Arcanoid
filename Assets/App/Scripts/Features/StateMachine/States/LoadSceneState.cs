using DG.Tweening;
using Features.UI.SceneTransitions;
using Module.Scenes;
using SceneReference;
using UnityEngine;

namespace Features.StateMachine.States
{
	public class LoadSceneState : State
	{
		[SerializeField] private SceneRef scene;
		[SerializeField] private SceneController sceneController;
		[SerializeField] private ISceneTransition sceneTransition;

		public override void Enter()
		{
			base.Enter();

			DOTween.KillAll();
			if (sceneTransition != null)
			{
				sceneTransition.PlayOn(() => sceneController.LoadScene(scene));
			}
			else
			{
				sceneController.LoadScene(scene);
			}
		}
	}
}