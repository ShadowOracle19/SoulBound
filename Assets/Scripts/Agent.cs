using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Tile currentTile;
    public int maxMoveTiles = 5;
    public int currentTilesLeft;
    public bool noMoreMovement = false;
    public bool currentlyBeingControlled = false;

    public GameObject hoverOver;
    public GameObject selected;

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Tile>() != null)
        {
            currentTile = other.gameObject.GetComponent<Tile>();
            currentTile.GetComponent<Tile>().objectOnTile = true;
            currentTile.GetComponent<Tile>().agentTile = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentTile.objectOnTile = false;
        currentTile.agentTile = null;
        currentTile = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTilesLeft = maxMoveTiles;
    }

    // Update is called once per frame
    void Update()
    {
        if (GridManager.Instance.tilesInMovement.Count == 0 && GridManager.Instance.whore == this && currentTilesLeft > 0)
        {
            currentTile.FindMoveableSquares();
        }
    }

    private void OnMouseDrag()
    {
        GridManager.Instance.movementIsDragging = true;
    }

    private void OnMouseUp()
    {
        GridManager.Instance.movementIsDragging = false;
        if (GridManager.Instance.tilesInMovement.Count != 0)
        {
            gameObject.transform.position = new Vector3(GridManager.Instance.tilesInMovement[GridManager.Instance.tilesInMovement.Count - 1].transform.position.x,
            transform.position.y, GridManager.Instance.tilesInMovement[GridManager.Instance.tilesInMovement.Count - 1].transform.position.z);
            GridManager.Instance.ClearListForMovement();
        }

    }

    private void OnMouseEnter()
    {
        hoverOver.SetActive(true);
    }

    private void OnMouseExit()
    {
        hoverOver.SetActive(false);
    }

    private void OnMouseDown()
    {
        GridManager.Instance.SelectAgent(this);
    }
}
