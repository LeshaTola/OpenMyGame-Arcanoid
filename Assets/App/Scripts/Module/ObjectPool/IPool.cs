namespace App.Scripts.Module.ObjectPool
{
	public interface IPool<T>
	{
		public T Get();
		public void Release(T obj);
	}
}