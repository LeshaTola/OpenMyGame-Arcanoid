namespace Scenes.Gameplay.Feature.Field
{
	public interface IFieldSizeProvider
	{
		GameField GameField { get; }

		GameField GetGameField();
	}
}