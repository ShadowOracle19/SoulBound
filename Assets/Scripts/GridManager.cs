using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private static GridManager _instance;
    public static GridManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("grid Manager is NULL");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    [SerializeField] private int _width, _height;
    [SerializeField] private int _bWidth, _bHeight;

    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Tile _backgroundTilePrefab;


    [SerializeField] private Transform _gridParent;
    [SerializeField] private Transform _backgroundGridParent;

    public GameObject target;


    private void Start()
    {
        GenerateGrid();
        GenerateBackgroundGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, 0 ,y), Quaternion.identity , _gridParent);
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
                spawnedTile.gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
            }
        }

    }

    void GenerateBackgroundGrid()
    {
        for (int x = 0; x < _bWidth; x++)
        {
            for (int y = 0; y < _bHeight; y++)
            {
                var spawnedTile = Instantiate(_backgroundTilePrefab, new Vector3(x-20, 0, y-40), Quaternion.identity, _backgroundGridParent);
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
                spawnedTile.gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
            }
        }

    }


}
