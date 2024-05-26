using System;
using UnityEngine;

namespace Features.ProjectCondition.Providers
{
	public class ProjectConditionProvider : MonoBehaviour, IProjectConditionProvider
	{
		public event Action OnApplicationStart;
		public event Action<bool> OnApplicationPaused;
		public event Action OnApplicationQuitted;

		private void Start()
		{
			OnApplicationStart?.Invoke();
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
