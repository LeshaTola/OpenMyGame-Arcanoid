namespace Module.Saves
{
	public interface IDataProvider<T>
	{
		void SaveData(T data);
		T GetData();
	}
}
