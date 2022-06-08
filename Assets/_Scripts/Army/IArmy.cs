using System.Collections.Generic;
using UnityEngine;

public interface IArmy
{

    public Faction Faction { get; set; }

    List<IUnit> Units { get; }

    List<GameObject> unitGameObjects { get; set; }

    bool IsAllDead { get; }

    public void CollectDeadUnits();

}

