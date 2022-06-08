using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private GridRepository _gridRepository;
    [SerializeField] private GridInteractor _gridInteractor;
    [SerializeField] private Transform _parent;
    [SerializeField] private float _offset;
    public void GenerateGrid()
    {
        _gridRepository.Tiles.Clear();
        _tilePrefab.GridInteractor = _gridInteractor;

        var tileSize = _tilePrefab.GetComponent<MeshRenderer>().bounds.size;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var position = new Vector3(x * (tileSize.x + _offset), 0, y * (tileSize.z + _offset));

                var tile = Instantiate(_tilePrefab, position, Quaternion.identity, _parent);

                tile.name = $"X: {x} Y: {y}";

                tile.Position = new Vector2(x, y);

                tile.ChangeColor(tile.StandartColor);

                _gridRepository.Tiles.Add(tile);
            }
        }
    }
}
