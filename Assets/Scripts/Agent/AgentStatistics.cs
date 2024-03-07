using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AgentData", menuName = "ScriptableObjects/CreateAgent", order = 1)]
public class AgentStatistics : ScriptableObject
{
    [Header("Agent Info")]
    public string AgentName;
    public string AgentDescription;
    public Sprite AgentSprite;
    public string BattleRole;

    [Header("Agent Stats")]
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
