using Scenes.Gameplay.Feature.LevelCreation;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Saves.Gameplay.DTOs.Level
{
	[Serializable]
	public struct LevelState
	{
		public LevelInfo levelInfo;
		public Dictionary<Vector2, int> blockHealth;
	}
}
