using Module.Saves.Structs;
using System;

namespace Features.Saves.Gameplay
{
	[Serializable]
	public struct BonusPosition
	{
		public string Id;
		public JsonVector2 Position;
	}
}
