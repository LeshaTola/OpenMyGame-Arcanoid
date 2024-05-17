﻿using Module.TimeProvider;
using Scenes.Gameplay.Feature.Player.PlayerInput;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class GameplayInstaller : MonoInstaller
	{
		[SerializeField] private Camera mainCamera;

		public override void InstallBindings()
		{
			BindTimeProvider();
			BindInput();
		}

		private void BindInput()
		{
			Container.BindInterfacesTo<MouseInput>()
				.AsSingle().WithArguments(mainCamera);
		}

		private void BindTimeProvider()
		{
			Container.Bind<ITimeProvider>()
				.To<GameplayTimeProvider>()
				.AsSingle();
		}
	}
}