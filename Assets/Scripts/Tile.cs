using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public int _x, _z;
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _available;
    [SerializeField] private GameObject _unavailable;

    public bool objectOnTile = false;
    public bool tileCanBeMovedOn = false;
    public Agent cock;


    public void Init(bool isOffset, int x, int z)
    {
        _x = x;
        _z = z;
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    private void Update()
    {
        if(objectOnTile) 
        { 
            _unavailable.SetActive(true);
            tileCanBeMovedOn = false;
        }
        else if(!objectOnTile)
        {
            _unavailable.SetActive(false);
        }

        if(tileCanBeMovedOn) 
        {
            _available.SetActive(true);
        }
        else if(!tileCanBeMovedOn)
        {
            _available.SetActive(false);
        }

        if(cock)
        {
            objectOnTile = true;

            if(cock.currentlyBeingControlled && (GridManager.Instance.whore.currentTilesLeft != 0))
            {
                FindMoveableSquares();

            }
            else if(cock.currentTilesLeft == 0 && !cock.noMoreMovement)
            {
                cock.noMoreMovement = true;
                ClearMoveableSquares();
            }
        }
    }

    public void EmptyTile()
    {
        cock = null;
        objectOnTile = false;
    }

    private void FindMoveableSquares()
    {
        if (GridManager.Instance.GetTileAtPosition(new Vector2(_x + 1, _z)) != null)
        {
            GridManager.Instance.GetTileAtPosition(new Vector2(_x + 1, _z)).tileCanBeMovedOn = true;

        }

        if (GridManager.Instance.GetTileAtPosition(new Vector2(_x, _z + 1)) != null)
        {
            GridManager.Instance.GetTileAtPosition(new Vector2(_x, _z + 1)).tileCanBeMovedOn = true;
        }

        if (GridManager.Instance.GetTileAtPosition(new Vector2(_x - 1, _z)) != null)
        {
            GridManager.Instance.GetTileAtPosition(new Vector2(_x - 1, _z)).tileCanBeMovedOn = true;

        }

        if (GridManager.Instance.GetTileAtPosition(new Vector2(_x, _z - 1)) != null)
        {
            GridManager.Instance.GetTileAtPosition(new Vector2(_x, _z - 1)).tileCanBeMovedOn = true;

        }
    }

    public void ClearMoveableSquares()
    {
        if(GridManager.Instance.GetTileAtPosition(new Vector2(_x + 1, _z)) != null)
        {
            GridManager.Instance.GetTileAtPosition(new Vector2(_x + 1, _z)).tileCanBeMovedOn = false;
        }
        
        if(GridManager.Instance.GetTileAtPosition(new Vector2(_x, _z + 1)) != null)
        {
            GridManager.Instance.GetTileAtPosition(new Vector2(_x, _z + 1)).tileCanBeMovedOn = false;
        }
        
        if(GridManager.Instance.GetTileAtPosition(new Vector2(_x - 1, _z)) != null)
        {
            GridManager.Instance.GetTileAtPosition(new Vector2(_x - 1, _z)).tileCanBeMovedOn = false;
        }
        
        if(GridManager.Instance.GetTileAtPosition(new Vector2(_x, _z - 1)) != null)
        {
            GridManager.Instance.GetTileAtPosition(new Vector2(_x, _z - 1)).tileCanBeMovedOn = false;
        }                                                                                 
        
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseDown()
    {
        if(this.tag == "Grid" && !objectOnTile && (GridManager.Instance.whore.currentTilesLeft != 0) && tileCanBeMovedOn)
        {
            Debug.Log(gameObject.name);
            GridManager.Instance.whore.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
            GridManager.Instance.whore.currentTilesLeft -= 1;
        }
        else
        {
            Debug.Log(gameObject.name);
        }
           
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

}
