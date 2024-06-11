using Features.StateMachine.Factories;
using Zenject;

namespace Features.Bootstrap
{
	public class StatesFactoriesInstaller : Installer<StatesFactoriesInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<IStatesFactory>().To<StatesFactory>().AsSingle();
			Container.Bind<IStateStepsFactory>().To<StateStepsFactory>().AsSingle();
		}
	}
}