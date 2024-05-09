using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.LevelCreation.Configs
{
	public class LevelConfig : ScriptableObject
	{
		[SerializeField] private float spacing;

		public float Spacing { get => spacing; }
	}
}