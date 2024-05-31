using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Configs
{
	[CreateAssetMenu(fileName = "BallVisualConfig", menuName = "Configs/Ball/Visual")]
	public class BallVisualConfig : ScriptableObject
	{
		[SerializeField] private Sprite defaultSprite;
		[SerializeField] private Sprite rageSprite;

		public Sprite DefaultSprite { get => defaultSprite; }
		public Sprite RageSprite { get => rageSprite; }
	}
}
