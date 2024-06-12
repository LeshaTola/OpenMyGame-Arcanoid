using Cysharp.Threading.Tasks;
using Scenes.Gameplay.Feature.Blocks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.LevelCreation
{
	public interface ILevelGenerator
	{
		Dictionary<Vector2Int, Block> Blocks { get; }

		event Action<Block> OnBlockDestroyed;

		UniTask DestroyLevelAsync();
		UniTask GenerateLevelAsync(LevelInfo levelInfo);
	}
}