using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.Field
{
	public class FieldDrawer : MonoBehaviour
	{
		[SerializeField] FieldController controller;

		private void OnDrawGizmos()
		{
			var gameField = controller.GetGameField();
			Gizmos.color = Color.yellow;

			Gizmos.DrawLine(gameField.TopLeftCorner, gameField.TopRightCorner);
			Gizmos.DrawLine(gameField.TopRightCorner, gameField.BottomRightCorner);
			Gizmos.DrawLine(gameField.BottomRightCorner, gameField.BottomLeftCorner);
			Gizmos.DrawLine(gameField.BottomLeftCorner, gameField.TopLeftCorner);
		}
	}
}
