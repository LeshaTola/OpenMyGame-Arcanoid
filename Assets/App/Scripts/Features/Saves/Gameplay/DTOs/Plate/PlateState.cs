using Module.Saves.Structs;
using System;
using System.Collections.Generic;

namespace Features.Saves.Gameplay.DTO.Plate
{
	[Serializable]
	public struct PlateState
	{
		public JsonVector2 Position;
		public List<JsonVector2> BallsLocalPositions;
	}
}
