using Cysharp.Threading.Tasks;
using Scenes.Gameplay.Feature.Blocks;
using System;

namespace Scenes.Gameplay.Feature.LevelCreation
{
	public interface ILevelGenerator
	{
		event Action<Block> OnBlockDestroyed;

		UniTask DestroyLevelAsync();
		UniTask GenerateLevelAsync(LevelInfo levelInfo);
	}
}