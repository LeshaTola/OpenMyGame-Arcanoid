using System.Collections.Generic;

namespace Module.Localization.Parsers
{
	public interface IParser
	{
		public Dictionary<string, string> Parse(string language, string localizationFile);
		public List<string> GetLanguages(string localizationFile);
	}
}
