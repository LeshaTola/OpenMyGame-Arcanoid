using Newtonsoft.Json;

namespace Module.Saves
{
	public class DataProvider<T> : IDataProvider<T>
	{
		private string key;
		private IStorage storage;

		public DataProvider(string key)
		{
			this.key = key;
		}

		public T GetData()
		{
			string json = storage.GetString(key);
			return JsonConvert.DeserializeObject<T>(json);
		}

		public void SaveData(T data)
		{
			string json = JsonConvert.SerializeObject(data);
			storage.SetString(key, json);
		}
	}
}
