using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealCommand : ICommand
{
    private GridInteractor gridInteractor;

    private IUnit unit;
    private IHealable targetUnit;

    private int maxHealthPower;

    private int healthPower;

    public HealCommand(IUnit unit, IHealable targetUnit, int maxHealthPower)
    {
        this.unit = unit;
        this.targetUnit = targetUnit;
        this.maxHealthPower = maxHealthPower;

        healthPower = maxHealthPower;
    }

    public void Execute()
    {

        var animator = unit.Animator;
        animator.SetBool("Cast Spell", true);

        if (((IUnit)targetUnit).Stats.Health + maxHealthPower > ((IUnit)targetUnit).Stats.MaxHealth)
        {
            healthPower = ((IUnit)targetUnit).Stats.MaxHealth - ((IUnit)targetUnit).Stats.Health;
        }

        targetUnit.Heal(healthPower);


        Debug.Log($"Юнит {unit} вылечил {targetUnit} на {healthPower} единиц здоровья");
    }

    public void Undo()
    {
        var _stats = ((IUnit)targetUnit).Stats;

        _stats.Health -= healthPower;
        ((IUnit)targetUnit).Stats = _stats;
    }

}

