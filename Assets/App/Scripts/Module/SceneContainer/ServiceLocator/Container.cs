using System.Collections.Generic;
using System.Linq;

namespace App.Scripts.Modules.SceneContainer.ServiceLocator
{
	public class Container : IServiceContainer
	{
		private readonly List<object> _values = new();

		public void Add(object value)
		{
			_values.Add(value);
		}

		public TBind GetService<TBind>() where TBind : class
		{
			return _values.FirstOrDefault() as TBind;
		}

		public IEnumerable<TBind> GetServices<TBind>() where TBind : class
		{
			foreach (var value in _values)
			{
				if (value is TBind bindValue)
				{
					yield return bindValue;
				}
			}
		}

		public object GetValue()
		{
			return _values.FirstOrDefault();
		}

		public IEnumerable<object> GetValues()
		{
			return _values;
		}
	}
}