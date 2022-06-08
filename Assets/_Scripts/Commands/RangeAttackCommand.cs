using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class RangeAttackCommand : ICommand
{
    private IUnit unit;
    private IUnit enemyUnit;

    private int maxDamage;
    private int damage;

  
    public RangeAttackCommand(IUnit unit, IUnit enemyUnit, int maxDamage)
    {
        this.unit = unit;
        this.enemyUnit = enemyUnit;
        this.maxDamage = maxDamage;
        damage = maxDamage;
    }

    
    public void Execute()
    {

        Debug.Log(unit);
        damage = maxDamage * (100 - enemyUnit.Stats.Defence) / 100;
        if (damage > enemyUnit.Stats.Health)
        {
            damage = enemyUnit.Stats.Health;
        }

        var unit_animator = unit.Animator;
        unit_animator.SetBool("Longbow Shoot Attack 01", true);

        enemyUnit.TakeDamage(damage);
        Debug.Log($"{unit} подстрелил {enemyUnit} и нанес {unit.Stats.AttackPower} урона");

    }

    public void Undo()
    {
        var _stats = enemyUnit.Stats;
        _stats.Health += damage;
        enemyUnit.Stats = _stats;
    }
}
