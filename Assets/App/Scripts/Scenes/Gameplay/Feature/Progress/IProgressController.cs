namespace App.Scripts.Scenes.Gameplay.Feature.Progress
{
	public interface IProgressController
	{
		int CalculateProgress();
		void ProcessProgress();
	}
}