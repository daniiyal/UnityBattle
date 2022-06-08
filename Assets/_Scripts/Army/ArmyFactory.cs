using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ArmyFactory : IArmyFactory
{

    ScriptableUnit[] scriptableUnits => Resources.LoadAll<ScriptableUnit>("ScriptableUnits");

    private int MinCost => scriptableUnits.Select(unit => unit.GetPrice).Min();


    public List<IUnit> CreateArmy(Faction faction, int money)
    {
        var unitMinCost = MinCost;
        var units = new List<IUnit>();

        System.Random random = new System.Random();


        while (money > 80)
        {
            var availableUnits = GetAvailableTypeOfUnits(money);
            Debug.Log(money);
            var randomIndex = random.Next(availableUnits.Length);
            
            var unitType = availableUnits[randomIndex];
            var unit = CreateUnit(faction, unitType);

            units.Add((IUnit)unit);

            var unitCost = scriptableUnits.Where(unit_type => unit_type.UnitType == unitType)
                                          .Select(unitCost => unitCost.GetPrice).First();

            money -= unitCost;
        }

        return units;
    }

    private UnitType[] GetAvailableTypeOfUnits(int maxCost)
    {
        return scriptableUnits.Where(unitConfigs => unitConfigs.GetPrice <= maxCost)
                              .Select(unitConfigs => unitConfigs.UnitType).ToArray();
    }

    private IUnit CreateUnit(Faction faction, UnitType unitType)
    {
        var creator = GetCreator(unitType);
        return creator.CreateUnit(faction);
    }

    private IUnitCreator GetCreator(UnitType unitType)
    {
        switch (unitType)
        {
            case UnitType.Infantry:
                return new InfantryUnitCreator();
            case UnitType.Archer:
                return new ArcherUnitCreator();
            case UnitType.Healer:
                return new HealerUnitCreator();
            case UnitType.Knight:
                return new KnightUnitCreator();
            case UnitType.Mage:
                return new WizardUnitCreator();
            case UnitType.TumbleWeed:
                return new TumbleWeedCreator();
            default:
                return null;
        }
    }

}

