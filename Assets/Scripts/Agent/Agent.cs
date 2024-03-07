using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : Token
{
    public AgentStatistics statistics;
    public int currentTilesLeft;
    public bool noMoreMovement = false;
    public bool currentlyBeingControlled = false;

    public GameObject selected;


    // Start is called before the first frame update
    void Start()
    {
        maxHealth = maxHealth * statistics.health;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);
        currentTilesLeft = maxMoveTiles + statistics.speed;
    }

    // Update is called once per frame
    private void Update()
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
        if (GridManager.Instance.tilesInMovement.Count == 0 && GridManager.Instance.whore == this && currentTilesLeft > 0)
        {
            currentTile.FindMoveableSquares();
        }
        currentHealth = (int)Mathf.Clamp(currentHealth, -Mathf.Infinity, maxHealth);
    }

    private void OnMouseDrag()
    {
        if(CombatManager.Instance.combatStarted)
            GridManager.Instance.movementIsDragging = true;
    }

    private void OnMouseUp()
    {
        //Drag drop mechanics
        GridManager.Instance.movementIsDragging = false;
        if (GridManager.Instance.tilesInMovement.Count != 0)
        {
            gameObject.transform.position = new Vector3(GridManager.Instance.tilesInMovement[GridManager.Instance.tilesInMovement.Count - 1].transform.position.x,
            transform.position.y, GridManager.Instance.tilesInMovement[GridManager.Instance.tilesInMovement.Count - 1].transform.position.z);
            GridManager.Instance.ClearListForMovement();
        }

    }

    private void OnMouseDown()
    {
        if(CombatManager.Instance.combatStarted)
        {
            if (canBeTargeted)
            {
                AbilityLoader.Instance.UseAbility(this);
                return;
            }

            GridManager.Instance.SelectAgent(this);

            AbilityLoader.Instance.LoadAgentAbilities(this);
        }
        
    }
}
