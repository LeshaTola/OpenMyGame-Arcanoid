using Features.StateMachine.States;

namespace Features.StateMachine.Factories
{
	public interface IStatesFactory
	{
		T GetState<T>() where T : State;
	}
}