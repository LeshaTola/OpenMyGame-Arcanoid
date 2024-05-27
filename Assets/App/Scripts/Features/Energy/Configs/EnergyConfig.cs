using UnityEngine;

namespace Features.Energy.Configs
{
	[CreateAssetMenu(fileName = "EnergyConfig", menuName = "Configs/Energy")]
	public class EnergyConfig : ScriptableObject
	{
		[SerializeField] private int maxEnergy;

		[SerializeField] private float recoveryTime;
		[SerializeField] private int recoveryEnergy;

		[SerializeField] private int playCost;
		[SerializeField] private int winReward;
		[SerializeField] private float continueCostMultiplier;

		public float RecoveryTime { get => recoveryTime; }
		public int MaxEnergy { get => maxEnergy; }
		public int RecoveryEnergy { get => recoveryEnergy; }
		public int PlayCost { get => playCost; }
		public float ContinueCostMultiplier { get => continueCostMultiplier; }
		public int WinReward { get => winReward; }
	}
}
