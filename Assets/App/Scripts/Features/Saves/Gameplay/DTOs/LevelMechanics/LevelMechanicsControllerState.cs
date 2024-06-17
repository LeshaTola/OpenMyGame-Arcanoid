using System;
using System.Collections.Generic;

namespace Features.Saves.Gameplay.DTO.LevelMechanics
{
	[Serializable]
	public class LevelMechanicsControllerState
	{
		public List<LevelMechanicsData> LevelMechanics = new();
	}
}
