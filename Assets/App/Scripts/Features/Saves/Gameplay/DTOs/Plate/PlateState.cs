using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Saves.Gameplay.DTO.Plate
{
	[Serializable]
	public struct PlateState
	{
		public Vector2 Position;
		public List<Vector3> BallsLocalPositions;
	}
}
