using NodeCanvas.BehaviourTrees;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/CreateEnemy", order = 1)]
public class EnemyStats : ScriptableObject
{
    public BehaviourTree enemyBehaviour;

    [Header("Enemy Info")]
    public string enemyName;
    public string agentDescription;
    public Sprite enemySprite;

    [Header("Enemy Stats")]
    public int attack;
    public int defense;
    public int health;
    public int alertnes; //initiative mod
    public int speed; //movement
    public int cost;
    public int critRate;

    [Header("Available Abilities")]
    public Ability ability1;
    public Ability ability2;
}
