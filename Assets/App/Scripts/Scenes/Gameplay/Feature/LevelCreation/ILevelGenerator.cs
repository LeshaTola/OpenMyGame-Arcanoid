using Scenes.Gameplay.Feature.Blocks;
using System;

namespace Scenes.Gameplay.Feature.LevelCreation
{
	public interface ILevelGenerator
	{
		event Action<Block> OnBlockDestroyed;

		void DestroyLevel();
		void GenerateLevel(LevelInfo levelInfo);
	}
}