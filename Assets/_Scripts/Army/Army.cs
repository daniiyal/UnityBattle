using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Army : IArmy
{
    public List<IUnit> Units { get; set; }

    public Faction Faction { get; set; }

    public bool IsAllDead
    {
        get
        {
            if (unitGameObjects.Count > 0)
                return false;
            else
                return true;
        }
    }
    public List<GameObject> unitGameObjects { get; set; }

    public Army(Faction faction, IArmyFactory factory, int cost)
    {
        Units = factory.CreateArmy(faction, cost);
        Faction = faction;
    }

    public void CollectDeadUnits()
    {
        var deadUnits = new List<KeyValuePair<int, GameObject>>();

        for (int i = 0; i < unitGameObjects.Count; i++)
        {
            var unit = unitGameObjects[i].GetComponent<IUnit>();
            if (!unit.isAlive)
            {
                var pair = new KeyValuePair<int, GameObject>(i, unitGameObjects[i]);
                deadUnits.Add(pair);

            }
        }

        if (deadUnits.Any())
        {
            var command = new CollectDeadUnitsCommand(this, deadUnits);
            GameManager.Instance.commandManager.Execute(command);
        }
    }

}

