using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Feature.Field
{
	public class WallBuilder : MonoBehaviour
	{
		[SerializeField] private BoxCollider2D wall;

		private IFieldSizeProvider fieldSizeProvider;

		[Inject]
		public void Init(IFieldSizeProvider fieldSizeProvider)
		{
			this.fieldSizeProvider = fieldSizeProvider;
			BuildWalls();
		}

		private void BuildWalls()// TODO: ???
		{
			var leftWall = Instantiate(wall, transform);

			leftWall.transform.position = new Vector2(fieldSizeProvider.GameField.MinX - leftWall.size.x / 2, 0);
			leftWall.transform.localScale = new Vector2(leftWall.transform.localScale.x, fieldSizeProvider.GameField.Height);

			var rightWall = Instantiate(wall, transform);

			rightWall.transform.position = new Vector2(fieldSizeProvider.GameField.MaxX + rightWall.size.x / 2, 0);
			rightWall.transform.localScale = new Vector2(rightWall.transform.localScale.x, fieldSizeProvider.GameField.Height);

			var topWall = Instantiate(wall, transform);

			topWall.transform.position = new Vector2(0, fieldSizeProvider.GameField.MaxY + topWall.size.y / 2);
			topWall.transform.localScale = new Vector2(fieldSizeProvider.GameField.Width, rightWall.transform.localScale.x);
		}

	}
}

