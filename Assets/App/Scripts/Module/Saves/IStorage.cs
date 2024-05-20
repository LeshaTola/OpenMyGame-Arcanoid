namespace Module.Saves
{
	public interface IStorage
	{
		void SetString(string key, string value);
		string GetString(string key);
	}

}