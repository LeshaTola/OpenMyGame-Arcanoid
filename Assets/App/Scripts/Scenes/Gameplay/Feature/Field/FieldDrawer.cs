using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Feature.Field
{
	public class FieldDrawer : MonoBehaviour
	{
		private IFieldSizeProvider fieldSizeProvider;

		[Inject]
		public void Init(IFieldSizeProvider fieldSizeProvider)
		{
			this.fieldSizeProvider = fieldSizeProvider;
		}

		private void OnDrawGizmos()
		{
			if (fieldSizeProvider == null)
			{
				return;
			}

			var gameField = fieldSizeProvider.GetGameField();
			Gizmos.color = Color.yellow;

			Gizmos.DrawLine(gameField.TopLeftCorner, gameField.TopRightCorner);
			Gizmos.DrawLine(gameField.TopRightCorner, gameField.BottomRightCorner);
			Gizmos.DrawLine(gameField.BottomRightCorner, gameField.BottomLeftCorner);
			Gizmos.DrawLine(gameField.BottomLeftCorner, gameField.TopLeftCorner);
		}
	}
}
