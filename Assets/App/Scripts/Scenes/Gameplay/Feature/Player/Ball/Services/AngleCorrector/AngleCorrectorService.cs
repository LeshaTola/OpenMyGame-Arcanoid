using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Ball.Services.AngleCorrector
{
	public class AngleCorrectorService : IAngleCorrectorService
	{
		private Vector2[] axes =
		{
			Vector2.up,
			Vector2.down,
			Vector2.left,
			Vector2.right
		};

		public Vector2 GetCorrectAngle(Vector2 vector, float minAngle)
		{
			foreach (Vector2 axis in axes)
			{
				float angle = Vector2.Angle(vector, axis);
				if (angle < minAngle)
				{
					float rotateAngle = minAngle - angle;
					var tempVector = Quaternion.Euler(0, 0, rotateAngle) * vector;
					float tempAngle = Vector2.Angle(tempVector, axis);
					if (tempAngle < minAngle)
					{
						tempVector = Quaternion.Euler(0, 0, -rotateAngle) * vector;
					}
					vector = tempVector;
					break;
				}
			}
			return vector;
		}
	}

}
