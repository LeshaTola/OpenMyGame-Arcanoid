using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Module.Localization.Configs
{
	[CreateAssetMenu(fileName = "LocalizationDictionary", menuName = "Dictionaries/Localization")]
	public class LocalizationDictionary : SerializedScriptableObject
	{
		[DictionaryDrawerSettings(KeyLabel = "Language Key", ValueLabel = "CSV File")]
		[SerializeField] private Dictionary<string, TextAsset> languages;

		public Dictionary<string, TextAsset> Languages { get => languages; }
	}
}