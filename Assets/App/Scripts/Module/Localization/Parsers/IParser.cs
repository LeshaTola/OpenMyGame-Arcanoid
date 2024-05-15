using System.Collections.Generic;

namespace Module.Localization.Parsers
{
	public interface IParser
	{
		public Dictionary<string, string> Parse(string localizationFile);
	}
}
