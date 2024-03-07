using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "ScriptableObjects/CreateAbility", order = 1)]
public class Ability : ScriptableObject
{
    [Header("Ability Info")]
    public string abilityName;
    public string abilityDescription;
    public Sprite abilitySprite;

    [Header("Ability Stats")]
    public bool isHealingAbility = false; //if this is set to true all attack will be switched to healing
    public bool isRanged = false; //if true the player will need to select the enemy to attack them
    public int damage;
    public int cost;
    public int numberOfHits;
}
