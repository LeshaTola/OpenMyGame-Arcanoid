using Scenes.Gameplay.Feature.Blocks;
using System;
using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.Progress
{
	public interface IProgressController
	{
		public event Action OnWin;

		int Progress { get; }
		float NormalizedProgress { get; }
		void CleanUp();
		void Init(List<Block> blocks);
		void ProcessProgress();
	}
}