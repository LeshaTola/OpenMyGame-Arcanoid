using Features.Bootstrap;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Field
{
	public class FieldController : MonoBehaviour, IInitializable
	{
		[SerializeField] private Camera mainCamera;
		[SerializeField] private Paddings paddings;
		[SerializeField] private BoxCollider2D wall;

		private float height;
		private float width;

		public GameField GameField { get; private set; }

		public void Init()
		{
			height = mainCamera.orthographicSize;
			width = height * mainCamera.aspect;

			GameField = GetGameField();

			GenerateWalls();
		}

		private void OnValidate()
		{
			height = mainCamera.orthographicSize;
			width = height * mainCamera.aspect;
		}

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

		private void GenerateWalls()// TODO: ???
		{
			var leftWall = Instantiate(wall, transform);

			leftWall.transform.position = new Vector2(GameField.MinX - leftWall.size.x / 2, 0);
			leftWall.transform.localScale = new Vector2(leftWall.transform.localScale.x, GameField.Height);

			var rightWall = Instantiate(wall, transform);

			rightWall.transform.position = new Vector2(GameField.MaxX + rightWall.size.x / 2, 0);
			rightWall.transform.localScale = new Vector2(rightWall.transform.localScale.x, GameField.Height);

			var topWall = Instantiate(wall, transform);

			topWall.transform.position = new Vector2(0, GameField.MaxY + topWall.size.y / 2);
			topWall.transform.localScale = new Vector2(GameField.Width, rightWall.transform.localScale.x);
		}
	}
}