using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.Field
{
	public struct GameField
	{
		public float MinX;
		public float MaxX;
		public float MinY;
		public float MaxY;

		public Vector2 TopLeftCorner => new(MinX, MaxY);
		public Vector2 TopRightCorner => new(MaxX, MaxY);
		public Vector2 BottomLeftCorner => new(MinX, MinY);
		public Vector2 BottomRightCorner => new(MaxX, MinY);
		public float Width => MaxX - MinX;

		public GameField(float minX, float maxX, float minY, float maxY)
		{
			MinX = minX;
			MaxX = maxX;
			MinY = minY;
			MaxY = maxY;
		}
	}
}