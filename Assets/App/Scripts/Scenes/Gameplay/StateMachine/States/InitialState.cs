using Features.StateMachine.States;
using Newtonsoft.Json;
using Scenes.Gameplay.Feature.LevelCreation;
using UnityEngine;

namespace Scenes.Gameplay.StateMachine.States
{
	public class InitialState : State
	{
		[SerializeField] private LevelGenerator levelGenerator;
		[SerializeField] private TextAsset fileLevelInfo;// TODO Move it to separate reader

		public override void Enter()
		{
			base.Enter();
			levelGenerator.GenerateLevel(GetLevelInfo());
			StateMachine.ChangeState<ResetState>();
		}

		private LevelInfo GetLevelInfo()
		{
			string json = fileLevelInfo.text;
			LevelInfo levelInfo = JsonConvert.DeserializeObject<LevelInfo>(json);
			return levelInfo;
		}
	}
}
