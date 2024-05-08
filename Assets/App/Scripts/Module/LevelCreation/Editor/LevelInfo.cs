using System;

namespace Module.LevelCreation
{
	[Serializable]
	public struct LevelInfo
	{
		public int Height;
		public int Width;
		public int[,] BlocksMatrix;
	}
}