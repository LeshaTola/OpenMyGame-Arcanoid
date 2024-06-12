using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Module.Localization.Configs
{
	[CreateAssetMenu(fileName = "LocalizationDictionary", menuName = "Dictionaries/Localization")]
	public class LocalizationDictionary : SerializedScriptableObject
	{
		[DictionaryDrawerSettings(KeyLabel = "Language Key", ValueLabel = "Path To CSV File")]
		[SerializeField] private Dictionary<string, string> languages;

		public Dictionary<string, string> Languages { get => languages; }
	}
}