using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Healer : Unit, ISpecialAbility
{

    public void DoSpecialAction()
    {
        System.Random random = new System.Random();
        var chance = random.Next(100 / Stats.Chance) == 0;

        if (chance)
        {
            var targetUnit = GetTargetUnit();
            if (targetUnit != null)
            {
                var command = new HealCommand(this, targetUnit, Stats.AttackPower);
                GameManager.Instance.commandManager.Execute(command);
            }
        }
    }

    private List<Tile> GetTargetTiles()
    {
        var gridInteractor = GameManager.Instance.gridInteractor;
        int index = gridInteractor.GridRepository.Tiles.IndexOf(currentTile);

        var currentTilePosition = currentTile.transform.position;

        var targetTiles = new List<Tile>();


        if(currentTilePosition.x - 1.1f > 0)
            targetTiles.Add(gridInteractor.GridRepository.Tiles[index - 9]);
        if(currentTilePosition.x + 1.1f < 21)
            targetTiles.Add(gridInteractor.GridRepository.Tiles[index + 9]);
        if (currentTilePosition.z - 1.1f > 0)
            targetTiles.Add(gridInteractor.GridRepository.Tiles[index - 1]);
        if (currentTilePosition.z + 1.1f < 9)
            targetTiles.Add(gridInteractor.GridRepository.Tiles[index + 1]);


        return targetTiles;

    }

    private IHealable GetTargetUnit()
    {

        IHealable targetUnit;

        var targetTiles = GetTargetTiles();

        foreach (var tile in targetTiles.ToList())
        {
            if (tile.Unit == null || tile.Unit.GetComponent<Unit>() is not IHealable)
            {
                targetTiles.Remove(tile);
            }
        }

        if (targetTiles.Count == 0)
            return null;
        System.Random random = new System.Random();
        var randomIndex = random.Next(targetTiles.Count);

        targetUnit = targetTiles[randomIndex].Unit.GetComponent<Unit>() as IHealable;
        return targetUnit;
    }
}