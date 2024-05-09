﻿namespace App.Scripts.Module.ObjectPool
{
	public interface IPooledObject
	{
		public void OnGet(IPool<IPooledObject> pool);
		public void OnRelease();
	}
}