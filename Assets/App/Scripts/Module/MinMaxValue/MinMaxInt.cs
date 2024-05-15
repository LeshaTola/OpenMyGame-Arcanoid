using System;

namespace Module.MinMaxValue
{
	[Serializable]
	public struct MinMaxInt
	{
		public int Min;
		public int Max;

		public bool IsValid(int value)
		{
			return (value >= Min) && (value <= Max);
		}

		public int GetRandom()
		{
			return UnityEngine.Random.Range(Min, Max);
		}
	}
}
