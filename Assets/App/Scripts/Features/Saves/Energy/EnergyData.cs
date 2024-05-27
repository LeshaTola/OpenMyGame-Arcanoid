using System;

namespace Features.Saves.Energy
{
	[Serializable]
	public class EnergyData
	{
		public DateTime ExitTime;
		public float RemainingRecoveryTime;
		public int Energy;
	}
}
