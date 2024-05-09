using UnityEngine;

namespace App.Scripts.Module.MinMaxValue
{
	public struct MinMaxValue
	{
		public float Min;
		public float Max;

		public bool IsValid(float value)
		{
			return (value >= Min) && (value <= Max);
		}

		public float GetRandom()
		{
			return Random.Range(Min, Max);
		}
	}
}
