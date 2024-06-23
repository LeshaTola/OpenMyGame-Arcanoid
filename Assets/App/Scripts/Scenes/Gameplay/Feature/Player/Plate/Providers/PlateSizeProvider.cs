using Scenes.Gameplay.Feature.Field;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Providers
{
	public class PlateSizeProvider : IPlateSizeProvider
	{
		private Plate plate;
		private IFieldSizeProvider fieldSizeProvider;

		public PlateSizeProvider(Plate plate, IFieldSizeProvider fieldSizeProvider)
		{
			this.plate = plate;
			this.fieldSizeProvider = fieldSizeProvider;
		}

		public Vector2 RightPosition => new(plate.transform.position.x + Width / 2, plate.transform.position.y);
		public Vector2 LeftPosition => new(plate.transform.position.x - Width / 2, plate.transform.position.y);
		public Vector2 CenterPosition => plate.transform.position;
		public float Width => plate.BoxCollider.size.x;

		public bool InBounds(Vector2 position)
		{
			return position.x >= LeftPosition.x && position.x <= RightPosition.x;
		}
	}
}
