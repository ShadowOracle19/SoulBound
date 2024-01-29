using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public GameObject currentTile;
    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Tile>() != null)
        {
            currentTile = other.gameObject;
            currentTile.GetComponent<Tile>().objectOnTile = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentTile.GetComponent<Tile>().objectOnTile = false;
        currentTile = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
