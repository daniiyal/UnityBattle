using System;
using UnityEngine;

public class TravelCommand : ICommand
{

    IUnit unit;
    GameObject unitGameObject;
    Tile currentTile;
    Tile targetTile;
    int currentTileIndex;
    GridRepository gridRepository;

    public TravelCommand(GameObject unit, Tile currentTile, Tile targetTile)
    {
        this.unit = unit.GetComponent<IUnit>();
        unitGameObject = unit;
        this.targetTile = targetTile;
        this.currentTile = currentTile;
        gridRepository = GameManager.Instance.gridInteractor.GridRepository;
        currentTileIndex = gridRepository.Tiles.IndexOf(currentTile);
    }

    public void Execute()
    {
        //Debug.Log(unit);

        if (unit.Faction == Faction.Left)
            targetTile = gridRepository.Tiles[currentTileIndex - 9 * unit.Stats.TravelDistance];
        else
            targetTile = gridRepository.Tiles[currentTileIndex + 9 * unit.Stats.TravelDistance];

        if (targetTile.Unit == null)
        {
            Move(targetTile);
        }
    }

    private void Move(Tile tile)
    {

        unit.Position = (new Vector3(   tile.transform.position.x, 
                                        tile.transform.position.y + 0.1f, 
                                        tile.transform.position.z));

        
        currentTile.Unit = null;
        currentTileIndex = gridRepository.Tiles.IndexOf(tile);

        tile.Unit = unitGameObject;
        unit.currentTile = tile;
        currentTile = tile;
        
    }

    public void Undo()
    {

        if (unit.Faction == Faction.Left)
            targetTile = gridRepository.Tiles[currentTileIndex + 9 * unit.Stats.TravelDistance];
        else
            targetTile = gridRepository.Tiles[currentTileIndex - 9 * unit.Stats.TravelDistance];
       
        if (targetTile.Unit == null)
        {
            Move(targetTile);
        }
    }
}
