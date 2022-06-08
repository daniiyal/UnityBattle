using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Archer : Unit, ISpecialAbility, IClonable
{
    public IUnit Clone()
    {
        var unitClone = (Unit)MemberwiseClone();
        return unitClone;
    }

    public void DoSpecialAction()
    {
        System.Random random = new System.Random();
        var chance = random.Next(100 / Stats.Chance) == 0;


        if (chance)
        {
            var enemyUnit = GetTargetUnit();
            if (enemyUnit != null)
            {
                var command = new RangeAttackCommand(this, enemyUnit, Stats.AttackPower);
                GameManager.Instance.commandManager.Execute(command);
            }

        }

    }


    private IUnit GetTargetUnit()
    {
        var gridInteractor = GameManager.Instance.gridInteractor;

        int index = gridInteractor.GridRepository.Tiles.IndexOf(this.currentTile);
        Tile targetTile;

        var targetUnits = new List<IUnit>();

        for (int i = 1; i < Stats.AttackRange; i++)
        {
            if (Faction == Faction.Left)
                
                targetTile = gridInteractor.GridRepository.Tiles[index - 9 * i];
            else
                targetTile = gridInteractor.GridRepository.Tiles[index + 9 * i];

            if (targetTile.Unit != null && targetTile.Unit.GetComponent<IUnit>().Faction != Faction)
            {
                targetUnits.Add(targetTile.Unit.GetComponent<IUnit>());
            }
        }

        if (targetUnits.Count == 0)
        {
            return null;
        }

        System.Random random = new System.Random();
        var enemyUnit = targetUnits[random.Next(targetUnits.Count)];

        return enemyUnit;
    }
}