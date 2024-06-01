using Scenes.Gameplay.Feature.LevelCreation.Mechanics;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.PackSelection.Feature.Packs.Configs
{
	[Serializable]
	public struct LevelSettings
	{
		[FoldoutGroup("@LevelName")]
		public string LevelName;
		[FoldoutGroup("@LevelName")]
		[SerializeReference]
		public List<LevelMechanics> LevelMechanics;
	}
}