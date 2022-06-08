using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wizard : Unit, ISpecialAbility
{


    public void DoSpecialAction()
    {
        System.Random random = new System.Random();
        var chance = random.Next(100 / Stats.Chance) == 0;
        IArmy army = GetArmy();

        var targetUnit = GetUnitClone();
        var positionForClone = GetLastUnitPosition();

        if (targetUnit == null || (positionForClone.x > 21 || positionForClone.x < 0))
        {
            return;
        }

        if (chance)
        { 
            var command = new CloneCommand(army, this, targetUnit, positionForClone);
            GameManager.Instance.commandManager.Execute(command);
        }
    }

    private IArmy GetArmy()
    {
        IArmy army;

        if (Faction == Faction.Left)
            army = GameManager.Instance.LeftArmy;
        else
            army = GameManager.Instance.RightArmy;

        return army;

    }

    private List<Tile> GetTargetTiles()
    {
        var gridInteractor = GameManager.Instance.gridInteractor;
        int index = gridInteractor.GridRepository.Tiles.IndexOf(currentTile);

        var currentTilePosition = currentTile.transform.position;

        var targetTiles = new List<Tile>();


        if (currentTilePosition.x - 1.1f > 0)
            targetTiles.Add(gridInteractor.GridRepository.Tiles[index - 9]);
        if (currentTilePosition.x + 1.1f < 21)
            targetTiles.Add(gridInteractor.GridRepository.Tiles[index + 9]);
        if (currentTilePosition.z - 1.1f > 0)
            targetTiles.Add(gridInteractor.GridRepository.Tiles[index - 1]);
        if (currentTilePosition.z + 1.1f < 9)
            targetTiles.Add(gridInteractor.GridRepository.Tiles[index + 1]);


        return targetTiles;

    }


    private IUnit GetUnitClone()
    {

        IClonable targetUnit;

        var targetTiles = GetTargetTiles();

        foreach (var tile in targetTiles.ToList())
        {
            if (tile.Unit == null || tile.Unit.GetComponent<Unit>() is not IClonable || tile.Unit.GetComponent<Unit>().Faction != Faction)
            {
                targetTiles.Remove(tile);
            }
        }

        if (targetTiles.Count == 0)
        {
            return null;
        }

        System.Random random = new System.Random();
        var randomIndex = random.Next(targetTiles.Count);

        targetUnit = targetTiles[randomIndex].Unit.GetComponent<Unit>() as IClonable;

        var unitClone = targetUnit.Clone();

        return unitClone;
    }

    private Vector3 GetLastUnitPosition()
    {
        IArmy army = GetArmy();

        IUnit lastUnit = army.unitGameObjects[army.unitGameObjects.Count - 1].GetComponent<IUnit>();

        Vector3 lastUnitPosition = lastUnit.Position;

        Vector3 positionForClone;

        if (Faction == Faction.Left)
            positionForClone = new Vector3(lastUnitPosition.x + 1.1f, 1, lastUnitPosition.z);
        else
            positionForClone = new Vector3(lastUnitPosition.x - 1.1f, 1, lastUnitPosition.z);

        return positionForClone;
    }
}