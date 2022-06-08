using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Strategy1VS1 : IStrategy
{
    public List<GameObject> spawnUnits(IArmy army)
    {
        return GameManager.Instance.unitManager.SpawnUnits1VS1(army);
    }

    public List<MeleeOpponents> MeleeAttackOpponentQueue(IArmy FirstArmy, IArmy SecondArmy)
    {
        var opponents = new MeleeOpponents(FirstArmy.unitGameObjects[0], SecondArmy.unitGameObjects[0]);

        var attackQueue = new List<MeleeOpponents> { opponents };
        
        return attackQueue;
    }

    public List<GameObject> GetUnitsWithSpecialAbility(IArmy FirstArmy, IArmy SecondArmy)
    {
        var specialUnits = new List<GameObject>();

        for (int i = 1; i < FirstArmy.unitGameObjects.Count; i++)
        {
            var unit = FirstArmy.unitGameObjects[i];
            if (unit.GetComponent<IUnit>() is ISpecialAbility)
            {
                specialUnits.Add(unit);
            }
        }


        for (int i = 1; i < SecondArmy.unitGameObjects.Count; i++)
        {
            var unit = SecondArmy.unitGameObjects[i];
            if (unit.GetComponent<IUnit>() is ISpecialAbility)
            {
                specialUnits.Add(unit);
            }
        }

        return specialUnits;
    }

    public void Reorganize(IArmy Army)
    {
        float i = 2.2f;
        foreach (var unit in Army.unitGameObjects)
        {
            if ((12.1f + i) <= 23.2f && (11f - i) >= -0.1f)
            {
                if (Army.Faction == Faction.Left)
                    unit.transform.position = new Vector3(12.1f + i, 1, 4.4f);
                else
                    unit.transform.position = new Vector3(11f - i, 1, 4.4f);
                unit.GetComponent<IUnit>().currentTile.Unit = null;
                unit.GetComponent<IUnit>().InitializeTile(unit);

                i += 1.1f;

            }
        }
    }

}