using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInteractor : MonoBehaviour
{
    public GridRepository GridRepository;

    public void SelectTile(Tile tile)
    {
        GridRepository.SelectedTile = tile;
        tile.State = State.Selected;
        tile.ChangeColor(tile.SelectedColor);


        //if (tile.Unit != null)
        //{
        //    tile.Unit.Variants = CalculateVariants(tile, tile.Unit.MoveVariants, tile.Unit.AttackVariants);

        //    ShowVariants(tile.Unit.Variants);
        //}
    }

    public void DeselectTile(Tile tile)
    {
        GridRepository.SelectedTile.State = State.Standart;
        GridRepository.SelectedTile.ChangeColor(GridRepository.SelectedTile.StandartColor);

        GridRepository.SelectedTile = null;

        //if (tile.Unit != null)
        //{
        //    ClearVariants(tile.Unit.Variants);
        //    tile.Unit.Variants.Clear();
        //}
    }

    public List<Tile> CalculateVariants(Tile UnitTile, List<Vector2> moveVariants, List<Vector2> attackVariants)
    {
        var variants = new List<Tile>();
        Tile tile;

        foreach (Vector2 offset in moveVariants)
        {
            tile = FindTileByPosition(offset, UnitTile, true);
            if (tile != null)
            {
                variants.Add(tile);
            }
        }

        foreach (Vector2 offset in attackVariants)
        {
            tile = FindTileByPosition(offset, UnitTile, false);
            if (tile != null)
            {
                variants.Add(tile);
            }

        }

        return variants;
    }

    public Tile FindTileByPosition(Vector2 offset, Tile UnitTile, bool MoveVariant)
    {
        if (MoveVariant)
        {
            return GridRepository.Tiles.Find(tile => tile.Position == UnitTile.Position + offset && tile.Unit == null);
        }
        else
        {
            return GridRepository.Tiles.Find(tile => tile.Position == UnitTile.Position + offset && (tile.Unit != null && tile.Unit.GetComponent<Unit>().Faction != UnitTile.Unit.GetComponent<Unit>().Faction));
        }
    }



    public void ShowVariants(List<Tile> variants)
    {
        foreach (Tile variant in variants)
        {
            variant.State = State.Variant;

            if (variant.Unit == null)
            {
                variant.ChangeColor(variant.MoveColor);
            }
            else
            {
                variant.ChangeColor(variant.AttackColor);
            }
        }
    }

    public void ClearVariants(List<Tile> variants)
    {
        foreach (Tile variant in variants)
        {
            variant.State = State.Standart;
            variant.ChangeColor(variant.StandartColor);
        }
    }
}
