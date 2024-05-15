using Features.StateMachine.States;
using Features.UI.SceneTransitions;
using Newtonsoft.Json;
using Scenes.Gameplay.Feature.LevelCreation;
using UnityEngine;

namespace Scenes.Gameplay.StateMachine.States
{
	public class InitialState : State
	{
		[SerializeField] private LevelGenerator levelGenerator;
		[SerializeField] private ISceneTransition sceneTransition;
		[SerializeField] private TextAsset fileLevelInfo;// TODO Move it to separate reader

		public override void Enter()
		{
			base.Enter();
			levelGenerator.GenerateLevel(GetLevelInfo());
			StateMachine.ChangeState<ResetState>();
		}

		public override void Exit()
		{
			base.Exit();
			sceneTransition.PlayOff();
		}

		private LevelInfo GetLevelInfo()
		{
			string json = fileLevelInfo.text;
			LevelInfo levelInfo = JsonConvert.DeserializeObject<LevelInfo>(json);
			return levelInfo;
		}

	}
}
