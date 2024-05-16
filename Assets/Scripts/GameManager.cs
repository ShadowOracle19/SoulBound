using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public EncounterBuilder encounter;

    //UI
    public GameObject battleUI;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this must play to load grid and everything
    public void LoadEncounter()
    {
        //this is just for combat to test if I can load combat encounters
        //Note to self: update this when dialogue is introduced
        CombatManager.Instance.LoadCombat(encounter.combat);
        GridManager.Instance.InitGrid();
        battleUI.SetActive(true);
    }
}
