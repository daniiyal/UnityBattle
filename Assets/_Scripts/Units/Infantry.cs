using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Infantry : Unit, IClonable, IHealable, ISpecialAbility
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
            var targetUnit = GetTargetUnit();

            ScriptableImprovement[] scriptableImprovements = Resources.LoadAll<ScriptableImprovement>("ScriptableImprovements");
            var improvementType = scriptableImprovements[random.Next(scriptableImprovements.Length)];

            var army = GetArmy();

            if (targetUnit != null && targetUnit.GetComponent<IImprovable>().CanImprove(improvementType.ImprovementType))
            {
                targetUnit.GetComponent<IImprovable>().improvements.Add(improvementType.ImprovementType);

                var command = new ImproveCommand(this, army, targetUnit, improvementType);
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

    private GameObject GetTargetUnit()
    {

        GameObject targetUnit;

        var targetTiles = GetTargetTiles();

        foreach (var tile in targetTiles.ToList())
        {
            if (tile.Unit == null || tile.Unit.GetComponent<IUnit>() is not IImprovable || tile.Unit.GetComponent<IUnit>().Faction != Faction)
            {
                targetTiles.Remove(tile);
            }
        }

        if (targetTiles.Count == 0)
            return null;

        System.Random random = new System.Random();
        var randomIndex = random.Next(targetTiles.Count);

        targetUnit = targetTiles[randomIndex].Unit;

        return targetUnit;
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

    public void Heal(int health)
    {
        var stats = Stats;
        stats.Health += health;
        SetStats(stats);
    }
}
