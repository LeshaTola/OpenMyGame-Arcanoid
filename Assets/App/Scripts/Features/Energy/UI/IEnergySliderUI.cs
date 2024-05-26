namespace Features.Energy.UI
{
	public interface IEnergySliderUI
	{
		void UpdateTimer(int totalSeconds);
		void UpdateUI(int currentValue, int maxValue);
	}
}