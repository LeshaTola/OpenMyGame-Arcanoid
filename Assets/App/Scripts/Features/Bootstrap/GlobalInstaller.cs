using Features.ProjectInitServices;
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
