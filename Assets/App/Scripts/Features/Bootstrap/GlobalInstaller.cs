using Module.Scenes;
using Zenject;

namespace Features.Bootstrap
{
	public class GlobalInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindSceneLoadService();
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
