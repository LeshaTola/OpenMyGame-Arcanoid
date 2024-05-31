﻿using Scenes.Gameplay.Feature.Blocks;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.LevelCreation.Providers.Level
{
	public interface ILevelProvider
	{
		Dictionary<Vector2Int, Block> Blocks { get; }

		void Init(Dictionary<Vector2Int, Block> blocks);
		void TurnOffColliders();
		void TurnOnColliders();
	}
}