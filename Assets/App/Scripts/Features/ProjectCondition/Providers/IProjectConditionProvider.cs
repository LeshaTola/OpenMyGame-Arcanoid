using System;

namespace Features.ProjectCondition.Providers
{
	public interface IProjectConditionProvider
	{
		event Action OnApplicationStart;
		event Action<bool> OnApplicationPaused;
		event Action OnApplicationQuitted;
	}
}