using Module.Saves.Structs;
using System;

namespace Features.Saves.Gameplay.DTO.LevelMechanics.Bird
{
	[Serializable]
	public class BirdLevelMechanicsData : LevelMechanicsData
	{
		public int Health;
		public JsonVector2 Position;
		public JsonVector2 TargetPosition;
	}
}
