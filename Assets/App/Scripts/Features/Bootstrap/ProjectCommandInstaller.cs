using Features.Commands;
using Zenject;

namespace Features.Bootstrap
{
	public class ProjectCommandInstaller : Installer<ProjectCommandInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<CloseCommand>().AsSingle().WithArguments("close");
		}
	}
}
