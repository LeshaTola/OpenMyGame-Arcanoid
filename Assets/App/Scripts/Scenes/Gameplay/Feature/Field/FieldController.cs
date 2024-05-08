using UnityEngine;

namespace Scenes.Gameplay.Feature.Field
{
	public class FieldController : MonoBehaviour
	{
		[SerializeField] private Camera mainCamera;
		[SerializeField] private Paddings paddings;

		public GameField GetGameFieldRect()
		{
			var height = mainCamera.orthographicSize;
			var width = height * mainCamera.aspect;
			return new GameField()
			{
				MinX = -width + paddings.Left,
				MaxX = width - paddings.Right,
				MinY = -height + paddings.Bottom,
				MaxY = height - paddings.Top,
			};
		}
	}
}