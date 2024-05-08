using Features.Bootstrap;
using Features.StateMachine;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace AssetsScenes.Gameplay.StateMachine
{
	public class GameplayStateMachine : MonoBehaviour, IInitializable
	{
		[SerializeField] private List<SerializableInterface<IUpdatable>> gamplayUpdatables = new();

		private Features.StateMachine.StateMachine core;

		public void Init()
		{
			core = new();
			core.AddState(new GameplayState(core, gamplayUpdatables));
			core.SetState<GameplayState>();
		}

		private void Update()
		{
			core.Update();
		}
	}
}