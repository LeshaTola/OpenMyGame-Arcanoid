using Features.Routers;
using Zenject;

namespace Features.Bootstrap
{
	public class ProjectRoutersInstaller : Installer<ProjectRoutersInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<IRouterShowInfoPopup>().To<RouterShowInfoPopup>().AsSingle();
		}
	}
}
