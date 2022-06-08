using System;
using UnityEngine;
using System.Linq;

public class RemoveImprovementCommand : ICommand
{
    GameObject targetUnitGameObject;
    GameObject improvementGameObject;
    IImprovable targetUnit;
    UnitImprove unitImprovement;
    ImprovementType improvement;

    public RemoveImprovementCommand(GameObject targetUnitGameObject)
    {
        this.targetUnitGameObject = targetUnitGameObject;
        targetUnit = targetUnitGameObject.GetComponent<IImprovable>();
        improvementGameObject = targetUnit.improvementGameObjects.Last();
        improvement = targetUnit.improvements.Last();

    }

    public void Execute()
    {
        unitImprovement = targetUnitGameObject.GetComponents<UnitImprove>().Last();

        GameManager.Instance.unitManager.DestroyUnit(unitImprovement);
        GameManager.Instance.unitManager.DestroyUnit(improvementGameObject);

        targetUnit.improvements.Remove(improvement);
        targetUnit.improvementGameObjects.Remove(improvementGameObject);

    }

    public void Undo()
    {
        var type = unitImprovement.GetType();
        targetUnitGameObject.AddComponent(type);

        GameManager.Instance.unitManager.SpawnItem(targetUnitGameObject, unitImprovement.improvement);

        targetUnit.improvementGameObjects.Add(improvementGameObject);
    }
}
