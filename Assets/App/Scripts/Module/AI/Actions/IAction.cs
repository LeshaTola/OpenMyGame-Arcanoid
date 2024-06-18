using Module.AI.Considerations;
using System.Collections.Generic;

namespace Module.AI.Actions
{
	public interface IAction
	{
		List<IConsideration> Considerations { get; }

		void Execute();
		float GetScore();
		void Init(List<IConsideration> considerations);
	}


}
