using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region dont touch this
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("GameManager is NULL");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion


    //UI
    public GameObject battleUI;
    public GameObject levelSelectUI;
    public GameObject dragDropUI;
    public GameObject InitParentUI;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this must play to load grid and everything
    public void LoadEncounter(EncounterBuilder encounter)
    {
        //this is just for combat to test if I can load combat encounters
        //Note to self: update this when dialogue is introduced
        CombatManager.Instance.LoadCombat(encounter.combat);
        GridManager.Instance.InitGrid();
        battleUI.SetActive(true);
        dragDropUI.SetActive(true);
        InitParentUI.SetActive(true);

        DeployAgent[] dragDropOptions = dragDropUI.GetComponentsInChildren<DeployAgent>();
        foreach (DeployAgent dragDropOption in dragDropOptions)
        {
            dragDropOption.ResetDragDrop();
        }
    }
}
