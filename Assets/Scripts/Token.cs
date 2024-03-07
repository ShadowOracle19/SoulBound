using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    public GameObject canBeTargetedSprite;

    [Header("Battle Stats")]
    public Tile currentTile;
    public int maxMoveTiles = 2;

    public int maxHealth = 10;
    public int currentHealth;

    public bool canBeTargeted = false;

    public GameObject hoverOver;

    public HealthBarBehaviour healthBar;

    public int initativeNumber;

    public void Update()
    {
        healthBar.SetHealth(currentHealth, maxHealth);
        if (canBeTargeted)
        {
            canBeTargetedSprite.SetActive(true);
        }
        else if (!canBeTargeted)
        {
            canBeTargetedSprite.SetActive(false);
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

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Tile>() != null)
        {
            currentTile = other.gameObject.GetComponent<Tile>();
            currentTile.GetComponent<Tile>().objectOnTile = true;
            currentTile.GetComponent<Tile>().currentToken = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentTile.objectOnTile = false;
        currentTile.currentToken = null;
        currentTile = null;
    }

    
}
