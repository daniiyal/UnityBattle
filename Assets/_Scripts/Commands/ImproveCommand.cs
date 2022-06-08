using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ImproveCommand : ICommand
{
    IUnit unit;
    IUnit targetUnit;
    
    GameObject targetUnitGameObject;
    ScriptableImprovement improvement;
    Stats targetUnitStats;

    GameObject item;
    Unit improvedUnit;


    public ImproveCommand(IUnit unit, IArmy army, GameObject targetUnit, ScriptableImprovement improvement)
    {
        this.unit = unit;
        this.targetUnit = targetUnit.GetComponent<IUnit>();
        targetUnitGameObject = targetUnit;
        targetUnitStats = this.targetUnit.Stats;
        this.improvement = improvement;
    }

    private Unit CreateImprovedUnit()
    {

        switch (improvement.ImprovementType)
        {
            case ImprovementType.Shield:
                improvedUnit = targetUnitGameObject.AddComponent<ShieldImprovement>();
                break;
            case ImprovementType.Spear:
                improvedUnit = targetUnitGameObject.AddComponent<SpearImprovement>();
                break;
            case ImprovementType.Helmet:
                improvedUnit = targetUnitGameObject.AddComponent<HelmetImprovement>();
                break;
            default:
                return null;
        }


        return improvedUnit;
    }


    public void Execute()
    { 

        var improvedUnit = CreateImprovedUnit();

        targetUnitGameObject.GetComponent<IUnit>().Stats = ((IUnit)improvedUnit).Stats;

        item = GameManager.Instance.unitManager.SpawnItem(targetUnitGameObject, improvement);

        targetUnitGameObject.GetComponent<IImprovable>().improvementGameObjects.Add(item);
   

    }

    public void Undo()
    {
        targetUnitGameObject.GetComponent<IUnit>().Stats = targetUnitStats;

        ((IImprovable)targetUnit).improvements.RemoveAt(((IImprovable)targetUnit).improvementGameObjects.IndexOf(item));

        targetUnitGameObject.GetComponent<IImprovable>().improvementGameObjects.Remove(item);

        GameManager.Instance.unitManager.DestroyUnit(improvedUnit);
        GameManager.Instance.unitManager.DestroyUnit(item);
        
        
    }
}