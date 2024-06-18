using Module.AI.Actions;
using System.Collections.Generic;

namespace Module.AI.Resolver
{
	public interface IActionResolver
	{
		IAction GetBestAction();
		void Init(List<IAction> actions);
	}
}
