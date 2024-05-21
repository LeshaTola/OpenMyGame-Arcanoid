using Features.Commands;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class CommandInstaller : Installer<CommandInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<RestartCommand>().AsSingle().WithArguments("Restart");
			Container.Bind<BackCommand>().AsSingle().WithArguments("Back");
			Container.Bind<ResumeCommand>().AsSingle().WithArguments("Resume");
		}
	}
}
