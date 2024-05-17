using DG.Tweening;
using Features.StateMachine.States.General;
using Features.UI.SceneTransitions;
using Module.Scenes;
using SceneReference;
using Zenject;

namespace Features.StateMachine.States
{
	public class LoadSceneState : State
	{
	}

	public class LoadSceneStateStep : StateStep
	{
		private SceneRef scene;
		private ISceneTransition sceneTransition;
		private ISceneLoadService sceneController;

		[Inject]
		public LoadSceneStateStep(SceneRef scene, ISceneTransition sceneTransition, ISceneLoadService sceneController)
		{
			this.scene = scene;
			this.sceneTransition = sceneTransition;
			this.sceneController = sceneController;
		}

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