using System;

namespace Scenes.Gameplay.Feature.Progress
{
	public interface IProgressController
	{
		public event Action OnWin;

		void ProcessProgress();
	}
}