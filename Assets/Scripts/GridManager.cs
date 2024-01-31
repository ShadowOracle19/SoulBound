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

    public Agent whore;
    public bool movementIsDragging = false;

    private Dictionary<Vector2, Tile> _tiles;

    public List<Tile> tilesInMovement = new List<Tile>();


    private void Start()
    {
        GenerateGrid();
        GenerateBackgroundGrid();
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, 0 ,y), Quaternion.identity , _gridParent);
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset, x, y);
                spawnedTile.gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);

                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

    }
    public Tile GetTileAtPosition(Vector2 pos)
    {
        if(_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
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
                spawnedTile.Init(isOffset, x, y);
                spawnedTile.gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
            }
        }

    }

    //check the tiles around the player and return true if there is an enemy
    public Tile CheckTilesAroundAgent()
    {
        int _x = whore.currentTile._x;
        int _z = whore.currentTile._z;
        if (GetTileAtPosition(new Vector2(_x + 1, _z)) != null && GetTileAtPosition(new Vector2(_x + 1, _z)).objectOnTile)
        {
            return GetTileAtPosition(new Vector2(_x + 1, _z));

        }

        if (GetTileAtPosition(new Vector2(_x, _z + 1)) != null && GetTileAtPosition(new Vector2(_x, _z + 1)).objectOnTile)
        {
            return GetTileAtPosition(new Vector2(_x, _z + 1));
        }

        if (GetTileAtPosition(new Vector2(_x - 1, _z)) != null && GetTileAtPosition(new Vector2(_x - 1, _z)).objectOnTile)
        {
            return GetTileAtPosition(new Vector2(_x - 1, _z));

        }

        if (GetTileAtPosition(new Vector2(_x, _z - 1)) != null && GetTileAtPosition(new Vector2(_x, _z - 1)).objectOnTile)
        {
            return GetTileAtPosition(new Vector2(_x, _z - 1));

        }
        Debug.Log("No enemies around");
        return null;
    }

    public void Attack()
    {
        if (CheckTilesAroundAgent() != null)
        {
            Tile enemyTile = CheckTilesAroundAgent();
            
            
            Destroy(enemyTile.cock.gameObject);
            enemyTile.EmptyTile();
        }
    }

    private void Update()
    {
        if(!movementIsDragging)
        {
            foreach (Transform child in _gridParent)
            {
                child.gameObject.GetComponent<Tile>().tileCanBeMovedOn = false;
            }

        }
    }

    public void SelectAgent(Agent agentToBeSelected)
    {
        if(whore != null)
        {
            //logic to remove moveable tiles go here
            whore.selected.SetActive(false);
            whore.currentlyBeingControlled = false;
            whore.currentTile.ClearMoveableSquares();
            whore = null;

        }

        whore = agentToBeSelected;
        whore.currentlyBeingControlled = true;
    }

    public void StartDragMovement(Tile tile)
    {
        if(tilesInMovement.Contains(tile) || tile.objectOnTile || (whore.currentTilesLeft == 0))
        {
            if (tilesInMovement.Count > 1 && tilesInMovement[tilesInMovement.Count - 2] == tile)
            {
                tilesInMovement[tilesInMovement.Count - 1].isHighlighted = false;
                tilesInMovement[tilesInMovement.Count - 1].tileCanBeMovedOn = false;
                tilesInMovement.RemoveAt(tilesInMovement.Count - 1);
                whore.currentTilesLeft += 1;
                tile.FindMoveableSquares();
                return;
            }
            else if(tilesInMovement.Count == 1 && tile.cock == whore)
            {
                tilesInMovement[tilesInMovement.Count - 1].isHighlighted = false;
                tilesInMovement[tilesInMovement.Count - 1].tileCanBeMovedOn = false;
                tilesInMovement.RemoveAt(tilesInMovement.Count - 1);
                whore.currentTilesLeft += 1;
                tile.FindMoveableSquares();
            }
            return;
        }


        tile.isHighlighted = true;
        tilesInMovement.Add(tile);
        whore.currentTilesLeft -= 1;
        tile.FindMoveableSquares();
    }

    public void ClearListForMovement()
    {
        foreach(Tile tile in tilesInMovement)
        {
            tile.isHighlighted = false;
            tile.tileCanBeMovedOn = false;
        }
        tilesInMovement.Clear();
    }
}
