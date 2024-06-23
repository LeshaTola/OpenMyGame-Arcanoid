using Module.AI.Actions;
using System.Collections.Generic;

namespace Module.AI.Resolver
{
	public class ActionResolver : IActionResolver
	{
		private List<IAction> actions = new();

		public void Init(List<IAction> actions)
		{
			this.actions = actions;
		}

		public IAction GetBestAction()
		{
			if (actions == null || actions.Count <= 0)
			{
				return null;
			}

			IAction bestAction = null;
			float bestScore = float.MinValue;

			foreach (IAction action in actions)
			{
				float score = action.GetScore();
				if (score > bestScore)
				{
					bestScore = score;
					bestAction = action;
				}
			}

			return bestAction;
		}
	}
}
