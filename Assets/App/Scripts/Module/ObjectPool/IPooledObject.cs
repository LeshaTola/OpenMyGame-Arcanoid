namespace Module.ObjectPool
{
	public interface IPooledObject
	{
		public void OnGet(IPool<IPooledObject> pool);
		public void Release();
		public void OnRelease();
	}
}
