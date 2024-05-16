using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterSelect : MonoBehaviour
{
    public EncounterBuilder encounter;
    
    public void LoadEncounter()
    {
        GameManager.Instance.LoadEncounter(encounter);
    }
}
