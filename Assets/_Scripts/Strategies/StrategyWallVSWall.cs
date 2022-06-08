using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StrategyWallVSWall : IStrategy
{

    public List<MeleeOpponents> MeleeAttackOpponentQueue(IArmy FirstArmy, IArmy SecondArmy)
    {

        var firstArmyUnitsCount = FirstArmy.unitGameObjects.Count;

        var attackQueue = new List<MeleeOpponents>();


        var z = 0.0f;

        for (int i = 0; i < firstArmyUnitsCount; i++)
        {
            var unit_left = FirstArmy.unitGameObjects.Where(x => x.GetComponent<IUnit>().Position.z == z).FirstOrDefault();
            var unit_right = SecondArmy.unitGameObjects.Where(x => x.GetComponent<IUnit>().Position.z == z).FirstOrDefault();

            if (unit_left != null && unit_right != null)
            {
                var opponents = new MeleeOpponents(unit_left, unit_right);
                attackQueue.Add(opponents);
            }

            if (z == 2.2f)
            {
                z = 3.3f;
            }
            else
            {
                z += 1.1f;
            }

        }

        return attackQueue;
    }

    public List<GameObject> GetUnitsWithSpecialAbility(IArmy FirstArmy, IArmy SecondArmy)
    {
        var firstArmyUnitsCount = FirstArmy.unitGameObjects.Count;
        var specialUnits = new List<GameObject>();


        var z = 0f;

        for (int i = 0; i < firstArmyUnitsCount; i++)
        {
            var unit_left = FirstArmy.unitGameObjects.Where(x => x.GetComponent<IUnit>().Position.z == z &&
                                                                 x.GetComponent<IUnit>() is ISpecialAbility).Skip(1);
            var unit_right = SecondArmy.unitGameObjects.Where(x => x.GetComponent<IUnit>().Position.z == z &&
                                                              x.GetComponent<IUnit>() is ISpecialAbility).Skip(1);
            if (unit_left != null && unit_right != null)
            {
                foreach (var unit in unit_left)
                {
                    specialUnits.Add(unit);
                }
                foreach (var unit in unit_right)
                {
                    specialUnits.Add(unit);
                }
            }

            if (z == 2.2f)
            {
                z = 3.3f;
            }
            else
            {
                z += 1.1f;
            }
        }

        return specialUnits;

    }

    public List<GameObject> spawnUnits(IArmy army)
    {
        return GameManager.Instance.unitManager.SpawnUnitsWallVSWall(army);
    }

    public void Reorganize(IArmy Army)
    {
        float i = 2.2f;
        float j = 0f;

        var unitGameObjects = new List<GameObject>();

        if ((12.1f + i) < 23.1f && (11f - i) > 0f)
        {

            foreach (var unit in Army.unitGameObjects)
            {
                if (Army.Faction == Faction.Left)
                    unit.transform.position = new Vector3(12.1f + i, 1, j);
                else
                    unit.transform.position = new Vector3(11f - i, 1, j);

                unit.GetComponent<IUnit>().currentTile.Unit = null;
                unit.GetComponent<IUnit>().InitializeTile(unit);


                if (j != 8.8f)
                    j += 1.1f;
                else
                {
                    i += 1.1f;
                    j = 0f;
                }

            }
        }

    }
}
