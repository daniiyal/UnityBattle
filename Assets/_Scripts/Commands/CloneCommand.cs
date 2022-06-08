using UnityEngine;


public class CloneCommand : ICommand
{
    GridInteractor gridInteractor;

    IUnit unit;

    Vector3 position;
    IUnit targetUnit;

    IArmy army;
    public CloneCommand(IArmy army, IUnit unit, IUnit targetUnit, Vector3 position)
    {
        this.army = army;
        this.unit = unit;
        this.targetUnit = targetUnit;
        this.position = position;
    }



    public void Execute()
    {
        var animator = unit.Animator;
        animator.SetBool("Cast Spell 02", true);

        army.unitGameObjects.Add(GameManager.Instance.unitManager.SpawnUnit((IUnit)targetUnit, position));

        Debug.Log($"Юнит {unit} клонировал {targetUnit}");

    }

    public void Undo()
    {
        var lastUnit = army.unitGameObjects[army.unitGameObjects.Count - 1];
        GameManager.Instance.unitManager.DestroyUnit(lastUnit);
        army.unitGameObjects.RemoveAt(army.unitGameObjects.Count - 1);
    }
}