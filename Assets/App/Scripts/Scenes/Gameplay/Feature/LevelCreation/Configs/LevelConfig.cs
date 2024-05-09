using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.LevelCreation.Configs
{
	[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Level")]
	public class LevelConfig : ScriptableObject
	{
		[SerializeField, Range(0,0.1f)] private float spacing;

		public float Spacing { get => spacing; }
	}
}