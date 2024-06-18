using Module.AI.Considerations;
using System.Collections.Generic;
using UnityEngine;

namespace Module.AI.Actions
{
	public abstract class Action : IAction
	{
		[SerializeField] private List<IConsideration> considerations = new();

		public List<IConsideration> Considerations { get => considerations; }

		public void Init(List<IConsideration> considerations)
		{
			this.considerations = considerations;
		}

		public abstract void Execute();

		public float GetScore()
		{
			if (considerations == null || considerations.Count <= 0)
			{
				return 0f;
			}

			float totalScore = 0;
			foreach (var consideration in considerations)
			{
				totalScore += consideration.GetScore();
			}
			totalScore /= considerations.Count;
			return totalScore;
		}
	}


}
