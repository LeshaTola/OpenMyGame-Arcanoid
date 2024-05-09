using App.Scripts.Features.UI.SceneTransitions;
using App.Scripts.Module.Scenes;
using DG.Tweening;
using SceneReference;
using UnityEngine;

namespace App.Scripts.Features.StateMachine.States
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