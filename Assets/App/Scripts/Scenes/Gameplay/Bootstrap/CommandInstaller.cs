using Scenes.Gameplay.Feature.Commands;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class CommandInstaller : Installer<CommandInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<RestartCommand>().AsSingle().WithArguments("restart");
			Container.Bind<BackCommand>().AsSingle().WithArguments("back");
			Container.Bind<ResumeCommand>().AsSingle().WithArguments("resume");
			Container.Bind<LoadNextLevelCommand>().AsSingle().WithArguments("next");
		}
	}
}
