using System.Linq;
using UnityEngine;

public class HealerUnitCreator : IUnitCreator
{

    public IUnit CreateUnit(Faction faction)
    {
        ScriptableUnit[] scriptableUnits;
        if (faction == Faction.Left)
            scriptableUnits = Resources.LoadAll<ScriptableUnit>("ScriptableUnits/LeftUnits");
        else
            scriptableUnits = Resources.LoadAll<ScriptableUnit>("ScriptableUnits/RightUnits");

        ScriptableUnit healerType = scriptableUnits.Where(unitType => unitType.UnitType == UnitType.Healer).First();


        GameObject gameObject = new GameObject(healerType.name);
        var unit = gameObject.AddComponent<Healer>();
        unit.ScriptableUnit = healerType;
        unit.Stats = healerType.BaseStats;

        return unit;
    }
}
