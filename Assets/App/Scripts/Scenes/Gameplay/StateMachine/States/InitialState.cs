using App.Scripts.Features.StateMachine.States;
using App.Scripts.Scenes.Gameplay.Feature.LevelCreation;
using Newtonsoft.Json;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.StateMachine.States
{
	public class InitialState: State
	{
		[SerializeField] private LevelGenerator levelGenerator;
		[SerializeField] private TextAsset fileLevelInfo;// TODO Move it to separate reader

		public override void Enter()
		{
			base.Enter();
			levelGenerator.GenerateLevel(GetLevelInfo());
			stateMachine.ChangeState<GameplayState>();
		}
		
		private LevelInfo GetLevelInfo()
		{
			string json = fileLevelInfo.text;
			LevelInfo levelInfo = JsonConvert.DeserializeObject<LevelInfo>(json);
			return levelInfo;
		}
	}
}
