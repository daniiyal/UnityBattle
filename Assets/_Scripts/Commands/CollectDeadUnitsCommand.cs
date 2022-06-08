using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class CollectDeadUnitsCommand : ICommand
{

    private IArmy army;

    private List<KeyValuePair<int, GameObject>> deadUnits;

    public CollectDeadUnitsCommand(IArmy army, List<KeyValuePair<int, GameObject>> deadUnits)
    {
        this.army = army;
        this.deadUnits = deadUnits;
    }


    public void Execute()
    {
        foreach (var pair in deadUnits)
        {
            Debug.Log($"{pair.Value} c ключом {pair.Key} умер");
            pair.Value.GetComponent<IUnit>().Die();
            army.unitGameObjects.Remove(pair.Value);
        }
    }

    public void Undo()
    {
        foreach (var pair in deadUnits)
        {
            army.unitGameObjects.Insert(pair.Key, pair.Value);

            pair.Value.SetActive(true);
            pair.Value.GetComponent<IUnit>().InitializeTile(pair.Value);
        }


    }
}