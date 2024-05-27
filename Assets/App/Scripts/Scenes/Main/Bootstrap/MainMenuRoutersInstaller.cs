using Scenes.Main.StateMachine.States.InitialState.Routers;
using Zenject;

namespace Scenes.Main.Bootstrap
{
	public class MainMenuRoutersInstaller : Installer<MainMenuRoutersInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<IRouterShowLanguages>().To<RouterShowLanguages>().AsSingle();
		}
	}
}