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
            currentTile.GetComponent<Tile>().cock = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentTile.ClearMoveableSquares();
        currentTile.objectOnTile = false;
        currentTile.cock = null;
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
        selected.SetActive(true);
        GridManager.Instance.SelectAgent(this);
    }
}
