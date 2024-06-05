using System;

namespace Features.Energy.Saves
{
	[Serializable]
	public class EnergyData
	{
		public DateTime ExitTime;
		public float RemainingRecoveryTime;
		public int Energy;
	}
}
