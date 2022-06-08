using System.Collections.Generic;


public interface IArmyFactory
{
    List<IUnit> CreateArmy(Faction faction, int money);
}

