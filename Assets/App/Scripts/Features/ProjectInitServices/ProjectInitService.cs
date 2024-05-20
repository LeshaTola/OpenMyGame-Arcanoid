using UnityEngine;

namespace Features.ProjectInitServices
{
	public class ProjectInitService : IProjectInitService
	{
		private int targetFrameRate;

		public ProjectInitService(int targetFrameRate)
		{
			this.targetFrameRate = targetFrameRate;
		}

		public void InitProject()
		{
			Application.targetFrameRate = targetFrameRate;
		}
	}
}
