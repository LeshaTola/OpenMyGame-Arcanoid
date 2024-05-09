namespace App.Scripts.Module.Localization.Localizers
{
	public interface ITextLocalizer
	{
		public void Init(LocalizationSystem localizationSystem);
		public void Translate();
	}
}
