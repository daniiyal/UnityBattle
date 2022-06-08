using System.Linq;
using UnityEngine;

public class ArcherUnitCreator : IUnitCreator
{

    public IUnit CreateUnit(Faction faction)
    {
        ScriptableUnit[] scriptableUnits;
        if (faction == Faction.Left)
            scriptableUnits = Resources.LoadAll<ScriptableUnit>("ScriptableUnits/LeftUnits");
        else
            scriptableUnits = Resources.LoadAll<ScriptableUnit>("ScriptableUnits/RightUnits");

        ScriptableUnit archerType = scriptableUnits.Where(unitType => unitType.UnitType == UnitType.Archer).First();

        GameObject gameObject = new GameObject(archerType.name);
        var unit = gameObject.AddComponent<Archer>();
        unit.ScriptableUnit = archerType;
        unit.Stats = archerType.BaseStats;

        return unit;

    }
}