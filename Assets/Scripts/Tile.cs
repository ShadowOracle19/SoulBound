using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public int _x, _z;
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _available;
    [SerializeField] private GameObject _unavailable;
    [SerializeField] private GameObject _blue;

    public bool objectOnTile = false;
    public bool tileCanBeMovedOn = false;
    public bool isHighlighted = false;

    public bool canPlacePlayerUnit = false;

    public Token currentToken;


    public void Init(bool isOffset, int x, int z)
    {
        _x = x;
        _z = z;
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    private void Update()
    {
        DisplayHideMoveSquares();

        
    }

    private void DisplayHideMoveSquares()
    {
        //shows possible areas where move line can be drawn
        if (tileCanBeMovedOn)
        {
            _blue.SetActive(true);
        }
        else if (!tileCanBeMovedOn)
        {
            _blue.SetActive(false);
        }

        //Shows where move line was drawn
        if (isHighlighted)
        {
            _available.SetActive(true);
        }
        else if (!isHighlighted)
        {
            _available.SetActive(false);
        }

        //Makes Tiles Unavailable to move onto
        if (currentToken)
        {
            objectOnTile = true;
        }
        else
        {
            objectOnTile = false;
        }

        if (objectOnTile)
        {
            _unavailable.SetActive(true);
            tileCanBeMovedOn = false;
        }
        else if (!objectOnTile)
        {
            _unavailable.SetActive(false);
        }
    }

    public void EmptyTile()
    {
        currentToken = null;
        objectOnTile = false;
    }

    public void FindMoveableSquares()
    {
        if(!objectOnTile || currentToken == GridManager.Instance.whore)
        {
            tileCanBeMovedOn = true;
        }

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
        if (!objectOnTile)
        {
            tileCanBeMovedOn = false;
        }

        if (GridManager.Instance.GetTileAtPosition(new Vector2(_x + 1, _z)) != null)
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
        if (GridManager.Instance.movementIsDragging /*&& tileCanBeMovedOn*/)
        {
            GridManager.Instance.StartDragMovement(this);
            return;
        }

        if (GridManager.Instance.dragToDeployAgent && !GridManager.Instance.deployAgentOverTile)
        {
            GridManager.Instance.deployAgentOverTile = true;
        }

        if (GridManager.Instance.dragToDeployAgent && GridManager.Instance.deployAgentOverTile && GridManager.Instance.deployingAgent != null && canPlacePlayerUnit)
        {
            GridManager.Instance.deployingAgent.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
        }
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
        if (GridManager.Instance.movementIsDragging)//When character is dragging movement line this will make sure it turns off blue highlight
        {
            ClearMoveableSquares();
            return;
        }
    }

}
