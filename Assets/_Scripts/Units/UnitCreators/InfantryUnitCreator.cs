using System.Linq;
using UnityEngine;

public class InfantryUnitCreator : IUnitCreator
{

    public IUnit CreateUnit(Faction faction)
    {
        ScriptableUnit[] scriptableUnits;
        if (faction == Faction.Left)
            scriptableUnits = Resources.LoadAll<ScriptableUnit>("ScriptableUnits/LeftUnits");
        else
            scriptableUnits = Resources.LoadAll<ScriptableUnit>("ScriptableUnits/RightUnits");

        ScriptableUnit infantryType = scriptableUnits.Where(unitType => unitType.UnitType == UnitType.Infantry).First();

        GameObject gameObject = new GameObject(infantryType.name);
        var unit = gameObject.AddComponent<Infantry>();
        unit.ScriptableUnit = infantryType;
        unit.Stats = infantryType.BaseStats;

        return unit;

    }
}

