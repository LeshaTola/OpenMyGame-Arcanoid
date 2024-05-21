using Features.ProjectInitServices;
using Features.Saves;
using Features.Saves.Keys;
using Module.Saves;
using Module.Scenes;
using Scenes.PackSelection.Feature.Packs;
using UnityEngine;
using Zenject;

namespace Features.Bootstrap
{
	public class GlobalInstaller : MonoInstaller
	{
		[SerializeField] private int targetFrameRate = 60;

		public override void InstallBindings()
		{
			Container.Bind<IStorage>().To<PlayerPrefsStorage>().AsSingle();
			Container.Bind<IDataProvider<PlayerProgressData>>()
				.To<DataProvider<PlayerProgressData>>()
				.AsSingle()
				.WithArguments(PlayerProgressDataKey.KEY);
			BindInitProjectService();
			BindSceneLoadService();
			BindPackProvider();
		}

		private void BindPackProvider()
		{
			Container.Bind<IPackProvider>()
				.To<PackProvider>()
				.AsSingle();
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
