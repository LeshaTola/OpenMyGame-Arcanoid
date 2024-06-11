using System;

namespace Features.ProjectCondition.Providers
{
	public interface IProjectConditionProvider
	{
		event Action<bool> OnApplicationFocused;
		event Action<bool> OnApplicationPaused;
		event Action OnApplicationQuitted;
	}
}