using UnityEngine;

namespace Scenes.Gameplay.Feature.Health.Configs
{
	[CreateAssetMenu(fileName = "HealthConfig", menuName = "Configs/Health")]
	public class HealthConfig : ScriptableObject
	{
		[SerializeField] private int health;
		[SerializeField] private int maxHealth;

		public int Health { get => health; }
		public int MaxHealth { get => maxHealth; }
	}
}
