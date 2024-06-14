using Module.MinMaxValue;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Configs
{
	[CreateAssetMenu(fileName = "BonusConfig", menuName = "Configs/Bonus/General")]
	public class BonusConfig : ScriptableObject
	{
		[SerializeField] private MinMaxFloat sizeMultiplier;

		public MinMaxFloat SizeMultiplier { get => sizeMultiplier; }
	}
}
