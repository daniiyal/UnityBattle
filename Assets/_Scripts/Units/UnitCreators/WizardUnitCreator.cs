using System.Linq;
using UnityEngine;

public class WizardUnitCreator : IUnitCreator
{

    public IUnit CreateUnit(Faction faction)
    {
        ScriptableUnit[] scriptableUnits;
        if (faction == Faction.Left)
            scriptableUnits = Resources.LoadAll<ScriptableUnit>("ScriptableUnits/LeftUnits");
        else
            scriptableUnits = Resources.LoadAll<ScriptableUnit>("ScriptableUnits/RightUnits");

        ScriptableUnit wizardUnit = scriptableUnits.Where(unitType => unitType.UnitType == UnitType.Mage).First();

        GameObject gameObject = new GameObject(wizardUnit.name);

        var unit = gameObject.AddComponent<Wizard>();
        unit.ScriptableUnit = wizardUnit;

        unit.Stats = wizardUnit.BaseStats;

        return unit;

    }
}
