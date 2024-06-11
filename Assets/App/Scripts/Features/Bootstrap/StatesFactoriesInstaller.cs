using Features.StateMachine.Factories;
using Features.StateMachine.States;
using Zenject;

namespace Features.Bootstrap
{
	public class StatesFactoriesInstaller : Installer<StatesFactoriesInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<IStatesFactory>().To<StatesFactory>().AsSingle();
			Container.Bind<IStateStepsFactory>().To<StateStepsFactory>().AsSingle();

			BindGlobalInitState();
		}

		private void BindGlobalInitState()
		{
			Container.Bind<GlobalInitialState>().AsSingle();
		}
	}
}