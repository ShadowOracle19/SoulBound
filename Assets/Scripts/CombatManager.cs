using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public List<Agent> agents = new List<Agent>();
    public List<Enemy> enemies = new List<Enemy>();
    public List<Token> initiative = new List<Token>();
    public Token currentInInitiative;

    public Transform enemyTransform;

    public int agentsToDeploy = 2;
    public GameObject deployUI;
    public GameObject InitParent;
    public int index;

    public GameObject initiativeImage;
    

    public bool combatStarted = false;

    #region Dont touch this
    private static CombatManager _instance;
    public static CombatManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("combat Manager is NULL");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(agentsToDeploy == 0)
        {
            deployUI.SetActive(false);
            StartCombat();
        }

        if (initiative[index].GetComponent<Agent>())
        {
            GridManager.Instance.SelectAgent((initiative[index].GetComponent<Agent>()));

            AbilityLoader.Instance.LoadAgentAbilities((initiative[index].GetComponent<Agent>()));
        }
    }

    public void StartCombat()
    {
        if (combatStarted)
            return;

        combatStarted = true;

        foreach(Transform child in enemyTransform)
        {
            enemies.Add(child.GetComponent<Enemy>());
        }

        PleaseRollForInitiative();
    }

    public void PleaseRollForInitiative()
    {
        foreach(Enemy enemy in enemies)
        {
            int initPos = Random.Range(1, 21);
            enemy.initativeNumber = initPos;
            initiative.Add(enemy);
        }

        foreach(Agent agent in agents)
        {
            int initPos = Random.Range(1, 21);
            agent.initativeNumber = initPos + agent.statistics.speed;
            initiative.Add(agent);
        }

        initiative = initiative.OrderBy(initiative => initiative.initativeNumber).ToList();

        for (int i = initiative.Count - 1; i >= 0; i--)
        {
            Image initImage = Instantiate(initiativeImage, InitParent.transform).GetComponent<Image>();

            if (initiative[i].GetComponent<Enemy>())
            {
                initImage.sprite = initiative[i].GetComponent<Enemy>().enemySprite;

            }
            else if(initiative[i].GetComponent<Agent>())
            {
                initImage.sprite = initiative[i].GetComponent<Agent>().statistics.AgentSprite;
            }
        }

        
        currentInInitiative = initiative[initiative.Count - 1];
        index = initiative.Count - 1;
    }

    public void EndTurn()
    {
        if(index == 0)
        {
            index = initiative.Count;
        }
        index -= 1;
        currentInInitiative = initiative[index];
    }


}
