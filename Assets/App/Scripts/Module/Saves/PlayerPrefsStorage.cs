using UnityEngine;

namespace Module.Saves
{
	public class PlayerPrefsStorage : IStorage
	{
		public string GetString(string key)
		{
			return PlayerPrefs.GetString(key, null);
		}

		public void SetString(string key, string value)
		{
			PlayerPrefs.SetString(key, value);
		}
	}

}