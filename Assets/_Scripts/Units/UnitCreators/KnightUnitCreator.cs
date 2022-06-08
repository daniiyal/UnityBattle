using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KnightUnitCreator : IUnitCreator
{

    public IUnit CreateUnit(Faction faction)
    {
        ScriptableUnit[] scriptableUnits;
        if (faction == Faction.Left)
            scriptableUnits = Resources.LoadAll<ScriptableUnit>("ScriptableUnits/LeftUnits");
        else
            scriptableUnits = Resources.LoadAll<ScriptableUnit>("ScriptableUnits/RightUnits");

        ScriptableUnit knightType = scriptableUnits.Where(unitType => unitType.UnitType == UnitType.Knight).First();

        GameObject gameObject = new GameObject(knightType.name);

        var unit = gameObject.AddComponent<Knight>();

        unit.ScriptableUnit = knightType;
        unit.Stats = knightType.BaseStats;



        return unit;

    }
}
