using UnityEngine;

namespace Scenes.Gameplay.Feature.LevelCreation.Mechanics.Bird
{
	[CreateAssetMenu(fileName = " BirdLevelMechanicsConfig", menuName = "Configs/Level/Mechanics/Bird")]
	public class BirdConfig : ScriptableObject
	{
		[SerializeField, Range(-0.5f, 0.5f)] private float yPosition;
		[SerializeField] private float xOffset;
		[SerializeField] private float speed;
		[SerializeField] private float frequency;
		[SerializeField] private float amplitude;
		[SerializeField] private float timeToRespawn;
		[SerializeField] private int health;

		public float YPosition { get => yPosition; }
		public float XOffset { get => xOffset; }
		public float Speed { get => speed; }
		public float TimeToRespawn { get => timeToRespawn; }
		public int Health { get => health; }
		public float Frequency { get => frequency; }
		public float Amplitude { get => amplitude; }
	}
}

