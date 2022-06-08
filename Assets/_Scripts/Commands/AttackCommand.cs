using System.Collections;
using UnityEngine;

public class AttackCommand : ICommand
{
    private IUnit unit;
    private IUnit enemyUnit;
    private int maxDamage;
    private int damage;

    public AttackCommand(IUnit unit, IUnit enemyUnit, int maxDamage)
    {
        this.unit = unit;
        this.enemyUnit = enemyUnit;
        this.maxDamage = maxDamage;
        damage = maxDamage;
    }

    public void Execute()
    {
        damage = maxDamage * (100 - enemyUnit.Stats.Defence) / 100;
        if (damage > enemyUnit.Stats.Health){
            damage = enemyUnit.Stats.Health;
        }

        var unit_animator = unit.Animator;
       
        unit_animator.SetTrigger("Melee Right Attack 01");

        //Debug.Log($"{unit} нанес {enemyUnit} - {damage} урона");
        
        enemyUnit.TakeDamage(damage);

    }


    public void Undo()
    {
       

        var _stats = enemyUnit.Stats;
        //Debug.Log(enemyUnit);
        _stats.Health += damage;

        enemyUnit.Stats = _stats;
    }

}

