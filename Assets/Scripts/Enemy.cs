using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Tile currentTile;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Tile>() != null)
        {
            currentTile = other.gameObject.GetComponent<Tile>();
            currentTile.GetComponent<Tile>().objectOnTile = true;
            currentTile.GetComponent<Tile>().enemy = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //currentTile.ClearMoveableSquares();
        currentTile.objectOnTile = false;
        currentTile.cock = null;
        currentTile = null;
    }
}
