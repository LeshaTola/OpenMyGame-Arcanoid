
using UnityEngine;

namespace Module.Saves.Structs
{
	public struct JsonVector2
	{
		public float X, Y;

		public JsonVector2(Vector3 vector)
		{
			X = vector.x;
			Y = vector.y;
		}

		public JsonVector2(Vector2 vector)
		{
			X = vector.x;
			Y = vector.y;
		}
	}
}
