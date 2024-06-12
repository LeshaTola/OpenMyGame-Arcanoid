using Scenes.Gameplay.Feature.LevelCreation;
using System;
using System.Collections.Generic;

namespace Features.Saves.Gameplay.DTOs.Level
{
	[Serializable]
	public struct LevelState
	{
		public LevelInfo levelInfo;
		public List<BlockData> BlocksData;
	}
}
