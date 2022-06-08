using UnityEngine;

public class ChangeStrategy : MonoBehaviour
{
    ICommand command;


    private void SetStrategy(IArmy firstArmy, IArmy secondArmy, IStrategy currentStrategy, IStrategy targetStrategy)
    {
        command = new ChangeStrategyCommand(firstArmy, secondArmy, currentStrategy, targetStrategy);
        GameManager.Instance.commandManager.Execute(command);
        GameManager.Instance.commandManager.EndTurn();
    }

    public void SetStrategy1VS1()
    {
        IStrategy currentStrategy = GameManager.Instance.Strategy;
        IStrategy strategy = new Strategy1VS1();
        if (strategy.GetType() != currentStrategy.GetType())
            SetStrategy(GameManager.Instance.LeftArmy, GameManager.Instance.RightArmy, currentStrategy, strategy);
         

    }
    public void SetStrategy3VS31()
    {
        IStrategy currentStrategy = GameManager.Instance.Strategy;
        IStrategy strategy = new Strategy3VS3();
        if (strategy.GetType() != currentStrategy.GetType())
            SetStrategy(GameManager.Instance.LeftArmy, GameManager.Instance.RightArmy, currentStrategy, strategy);
    }
    public void SetStrategyWallVSWall()
    {
        IStrategy currentStrategy = GameManager.Instance.Strategy;
        IStrategy strategy = new StrategyWallVSWall();
        if (strategy.GetType() != currentStrategy.GetType())
            SetStrategy(GameManager.Instance.LeftArmy, GameManager.Instance.RightArmy, currentStrategy, strategy);
    }

}