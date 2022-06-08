using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ChangeStrategyCommand : ICommand
{
    private IStrategy initStrategy;
    private IStrategy targetStrategy;
    private IArmy FirstArmy;
    private IArmy SecondArmy;

    public ChangeStrategyCommand(IArmy FirstArmy, IArmy SecondArmy, IStrategy initStrategy, IStrategy targetStrategy)
    {
        this.initStrategy = initStrategy;
        this.targetStrategy = targetStrategy;

        this.FirstArmy = FirstArmy;
        this.SecondArmy = SecondArmy;
    }



    public void Execute()
    {

        GameManager.Instance.Strategy = targetStrategy;
        targetStrategy.Reorganize(FirstArmy);
        targetStrategy.Reorganize(SecondArmy);
    }

    public void Undo()
    {
        GameManager.Instance.Strategy = initStrategy;
        initStrategy.Reorganize(FirstArmy);
        initStrategy.Reorganize(SecondArmy);
    }
}
