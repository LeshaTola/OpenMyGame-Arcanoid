using Features.StateMachine.States.General;
using Zenject;

namespace Features.StateMachine.Factories
{
	public class StateStepsFactory : IStateStepsFactory
	{
		DiContainer diContainer;

		public StateStepsFactory(DiContainer diContainer)
		{
			this.diContainer = diContainer;
		}

		public T GetStateStep<T>() where T : StateStep
		{
			return diContainer.Resolve<T>();
		}
	}
}
