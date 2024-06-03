using System;

namespace Scenes.Gameplay.Feature.LevelCreation
{
	[Serializable]
	public struct LevelInfo
	{
		public int Height;
		public int Width;
		public int[,] BlocksMatrix;
		public string[,] BonusesMatrix;
	}
}