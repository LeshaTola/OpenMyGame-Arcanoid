using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.Reset.Services
{
	public class ResetService : IResetService
	{
		private List<IResetable> resetables;

		public ResetService(List<IResetable> resetables)
		{
			this.resetables = resetables;
		}

		public void Reset()
		{
			foreach (var resetable in resetables)
			{
				resetable.Reset();
			}
		}
	}
}
