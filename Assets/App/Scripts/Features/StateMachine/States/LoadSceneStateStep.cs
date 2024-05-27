using DG.Tweening;
using Features.StateMachine.States.General;
using Features.UI.SceneTransitions;
using Module.Scenes;
using SceneReference;

namespace Features.StateMachine.States
{
	public class LoadSceneStateStep : StateStep
	{
		private SceneRef scene;
		private ISceneTransition sceneTransition;
		private ISceneLoadService sceneController;

		public LoadSceneStateStep(ISceneTransition sceneTransition, ISceneLoadService sceneController)
		{
			this.sceneTransition = sceneTransition;
			this.sceneController = sceneController;
		}

		public SceneRef Scene { get => scene; set => scene = value; }

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