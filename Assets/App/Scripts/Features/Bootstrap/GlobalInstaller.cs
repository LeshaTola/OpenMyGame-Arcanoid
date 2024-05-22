using Features.ProjectInitServices;
using Features.Saves;
using Features.Saves.Keys;
using Module.Saves;
using Module.Scenes;
using Scenes.PackSelection.Feature.Packs;
using Scenes.PackSelection.Feature.Packs.Configs;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Features.Bootstrap
{
	public class GlobalInstaller : MonoInstaller
	{
		[SerializeField] private int targetFrameRate = 60;
		[SerializeField] private List<Pack> packs;

		public override void InstallBindings()
		{
			BindStorage();
			BindDataProvider();
			BindInitProjectService();
			BindSceneLoadService();
			BindPackProvider();
		}

		private void BindStorage()
		{
			Container.Bind<IStorage>().To<PlayerPrefsStorage>().AsSingle();
		}

		private void BindDataProvider()
		{
			Container.Bind<IDataProvider<PlayerProgressData>>()
				.To<DataProvider<PlayerProgressData>>()
				.AsSingle()
				.WithArguments(PlayerProgressDataKey.KEY);
		}

		private void BindPackProvider()
		{
			Container.Bind<IPackProvider>()
				.To<PackProvider>()
				.AsSingle()
				.WithArguments(packs);
		}

		private void BindInitProjectService()
		{
			Container.Bind<IProjectInitService>()
							.To<ProjectInitService>()
							.AsSingle()
							.WithArguments(targetFrameRate);
		}

		private void BindSceneLoadService()
		{
			Container
				.Bind<ISceneLoadService>()
				.To<SceneLoadService>()
				.AsSingle();
		}
	}
}
