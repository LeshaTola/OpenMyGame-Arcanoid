using UnityEngine;

namespace Scenes.Gameplay.Feature.LevelCreation.Configs
{
	[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Level")]
	public class LevelConfig : ScriptableObject
	{
		[SerializeField, Range(0, 0.1f)] private float spacing;
		[SerializeField] private float buildingTime;

		public float Spacing { get => spacing; }
		public float BuildingTime { get => buildingTime; }
	}
}