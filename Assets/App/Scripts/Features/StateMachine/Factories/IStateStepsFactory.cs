using Features.StateMachine.States.General;

namespace Features.StateMachine.Factories
{
	public interface IStateStepsFactory
	{
		T GetStateStep<T>() where T : StateStep;
	}
}