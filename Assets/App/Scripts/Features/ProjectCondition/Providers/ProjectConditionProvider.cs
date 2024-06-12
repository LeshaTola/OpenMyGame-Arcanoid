using System;
using UnityEngine;

namespace Features.ProjectCondition.Providers
{
	public class ProjectConditionProvider : MonoBehaviour, IProjectConditionProvider
	{
		public event Action<bool> OnApplicationFocused;
		public event Action<bool> OnApplicationPaused;
		public event Action OnApplicationQuitted;


		private void OnApplicationFocus(bool focus)
		{
			OnApplicationFocused?.Invoke(focus);
		}

		private void OnApplicationPause(bool pause)
		{
			OnApplicationPaused?.Invoke(pause);
		}

		private void OnApplicationQuit()
		{
			OnApplicationQuitted?.Invoke();
		}
	}
}
