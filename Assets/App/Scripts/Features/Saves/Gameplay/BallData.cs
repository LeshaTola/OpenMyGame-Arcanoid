﻿using System;
using UnityEngine;

namespace Features.Saves.Gameplay
{
	[Serializable]
	public struct BallData
	{
		public Vector3 Position;
		public Vector3 Direction;
		public bool IsOnPlate;
	}
}
