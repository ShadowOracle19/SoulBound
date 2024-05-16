using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatData", menuName = "Encounter/CreateCombatEncounter", order = 2)]

public class CombatCreator : ScriptableObject
{
    public List<Combatent> combatents = new List<Combatent>();
}

[System.Serializable]
public struct Combatent
{
    public GameObject enemy;

    public Vector3 position;
}
