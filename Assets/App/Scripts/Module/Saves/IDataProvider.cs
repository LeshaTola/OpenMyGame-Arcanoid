namespace Module.Saves
{
	public interface IDataProvider<T> where T : class
	{
		void SaveData(T data);
		T GetData();
		bool HasData();
	}
}
