using Features.StateMachine.States;
using Zenject;

namespace Features.StateMachine.Factories
{
	public class StatesFactory : IStatesFactory
	{
		DiContainer diContainer;

		public StatesFactory(DiContainer diContainer)
		{
			this.diContainer = diContainer;
		}

		public T GetState<T>() where T : State
		{
			T state = diContainer.Resolve<T>();
			state.Init(diContainer.Resolve<StateMachine>());
			return state;
		}
	}
}
