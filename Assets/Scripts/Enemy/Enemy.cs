using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Token
{
    public EnemyStats stats;
    private Blackboard blackboard;


    private void Start()
    {
        blackboard = this.GetComponent<Blackboard>();
        currentHealth = maxHealth * stats.health;
        healthBar.SetHealth(currentHealth, maxHealth);
        maxMoveTiles += stats.speed;
        currentTilesLeft = maxMoveTiles;
    }

    private void FixedUpdate()
    {
        blackboard.SetVariableValue("TurnStarted", currentTurn);
    }

    public void OnMouseDown()
    {
        if (canBeTargeted)
        {
            AbilityLoader.Instance.UseAbility(this);
            return;
        }
    }

    public Agent CheckIfAgentInRange()
    {
        foreach(Agent agent in CombatManager.Instance.agents)
        {
            Debug.Log(agent.name + " " + ((int)(Vector3.Distance(gameObject.transform.position, agent.transform.position))));
            if((int)(Vector3.Distance(gameObject.transform.position, agent.transform.position)) <= 1)
            {
                Debug.Log(agent.name + " is in range");
                return agent;
            }
            else
            {
                continue;
            }
        }

        return null;
    }

    public Agent FindClosestAgent()
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Agent agent in CombatManager.Instance.agents)
        {
            float dist = Vector3.Distance(agent.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = agent.transform;
                minDist = dist;
            }
        }
        return tMin.gameObject.GetComponent<Agent>();
    }

}
