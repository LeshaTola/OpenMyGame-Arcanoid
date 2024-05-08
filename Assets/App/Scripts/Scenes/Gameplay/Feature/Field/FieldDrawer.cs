using UnityEngine;

namespace Scenes.Gameplay.Feature.Field
{
	public class FieldDrawer : MonoBehaviour
	{
		[SerializeField] FieldController controller;

		private void OnDrawGizmos()
		{
			var gameField = controller.GetGameFieldRect();
			Gizmos.color = Color.yellow;

			Vector2 topLeft = new Vector2(gameField.MinX, gameField.MaxY);
			Vector2 topRight = new Vector2(gameField.MaxX, gameField.MaxY);
			Vector2 bottomLeft = new(gameField.MinX, gameField.MinY);
			Vector2 bottomRight = new Vector2(gameField.MaxX, gameField.MinY);

			Gizmos.DrawLine(topLeft, topRight);
			Gizmos.DrawLine(topRight, bottomRight);
			Gizmos.DrawLine(bottomRight, bottomLeft);
			Gizmos.DrawLine(bottomLeft, topLeft);
		}
	}
}
