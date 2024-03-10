using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    public GameObject canBeTargetedSprite;

    [Header("Battle Stats")]
    public Tile currentTile;
    public int maxMoveTiles = 2;
    public int currentTilesLeft;

    public int maxHealth = 10;
    public int currentHealth;

    public bool canBeTargeted = false;

    public GameObject hoverOver;

    public HealthBarBehaviour healthBar;

    public int initativeNumber;

    public bool currentTurn = false;

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

        Death();
    }

    public void Death()
    {
        if (currentHealth <= 0)//death
        {
            CombatManager.Instance.KillOffToken(this);
            Destroy(gameObject);
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

        other.GetComponent<Tile>().objectOnTile = false;
        other.GetComponent<Tile>().currentToken = null;
        currentTile = null;
        
    }

    
}
