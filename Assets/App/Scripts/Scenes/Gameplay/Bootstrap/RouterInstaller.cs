using Scenes.Gameplay.StateMachine.States.Loss.Routers;
using Scenes.Gameplay.StateMachine.States.Win.Routers;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class RouterInstaller : Installer<RouterInstaller>
	{
		public override void InstallBindings()
		{
			CommandInstaller.Install(Container);

			Container.Bind<IRouterShowLoss>().To<RouterShowLoss>().AsSingle();
			Container.Bind<IRouterShowMenu>().To<RouterShowMenu>().AsSingle();
			Container.Bind<IRouterShowWin>().To<RouterShowWin>().AsSingle();
		}
	}
}
