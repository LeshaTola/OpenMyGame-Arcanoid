using System;

namespace Features.Saves.Gameplay.DTOs.Progress
{
	[Serializable]
	public struct ProgressState
	{
		public int StartBlockCount;
		public int CurrentBlockCount;
	}
}
