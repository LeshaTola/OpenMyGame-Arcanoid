using Module.Saves.Structs;
using System;

namespace Features.Saves.Gameplay.DTO.Balls
{
	[Serializable]
	public struct BallData
	{
		public JsonVector2 Position;
		public JsonVector2 Direction;
	}
}
