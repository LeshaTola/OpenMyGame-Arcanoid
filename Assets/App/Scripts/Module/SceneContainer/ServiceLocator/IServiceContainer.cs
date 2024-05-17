using System.Collections.Generic;

namespace App.Scripts.Modules.SceneContainer.ServiceLocator
{
	public interface IServiceContainer
	{
		object GetValue();
		IEnumerable<object> GetValues();
	}
}