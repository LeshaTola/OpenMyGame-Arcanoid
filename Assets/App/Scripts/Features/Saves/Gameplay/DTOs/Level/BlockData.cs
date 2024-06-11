using Module.Saves.Structs;
using System;

namespace Features.Saves.Gameplay.DTOs.Level
{
	[Serializable]
	public struct BlockData
	{
		public JsonVector2Int Position;
		public int Health;
	}
}
