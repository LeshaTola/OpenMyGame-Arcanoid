using Scenes.Gameplay.Feature.Blocks;
using System;
using System.Collections.Generic;

namespace Scenes.Gameplay.Feature.Progress
{
	public interface IProgressController
	{
		public event Action OnWin;

		void CleanUp();
		void Init(List<Block> blocks);
		void ProcessProgress();
	}
}