using System;
using UnityEngine;

namespace Module.MinMaxValue
{
	[Serializable]
	public struct MinMaxFloat
	{
		public float Min;
		public float Max;

		public bool IsValid(float value)
		{
			return (value >= Min) && (value <= Max);
		}

		public float GetRandom()
		{
			return UnityEngine.Random.Range(Min, Max);
		}

		public float Clamp(float value)
		{
			return Mathf.Clamp(value, Min, Max);
		}
	}
}
