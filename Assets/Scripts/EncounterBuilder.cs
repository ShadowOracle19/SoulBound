using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EncounterData", menuName = "Encounter/CreateEncounter", order = 2)]
public class EncounterBuilder : ScriptableObject
{
    public CombatCreator combat;
}
