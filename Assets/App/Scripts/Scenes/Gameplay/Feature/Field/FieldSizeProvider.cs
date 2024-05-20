using UnityEngine;

namespace Scenes.Gameplay.Feature.Field
{
	public class FieldSizeProvider : IFieldSizeProvider
	{
		private Paddings paddings;

		private float height;
		private float width;

		public FieldSizeProvider(Camera mainCamera, Paddings paddings)
		{
			this.paddings = paddings;

			height = mainCamera.orthographicSize;
			width = height * mainCamera.aspect;

			GameField = GetGameField();
		}

		public GameField GameField { get; private set; }

		public GameField GetGameField()
		{
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

