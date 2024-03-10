using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityLoader : MonoBehaviour
{
    public TextMeshProUGUI abilityName1;
    public TextMeshProUGUI abilityName2;
    public TextMeshProUGUI abilityNameNull;

    public Agent currentSelectedAgent;
    public Ability currentAbility;

    public List<Agent> friendlyTargets = new List<Agent>();
    public List<Enemy> enemyTargets = new List<Enemy>();

    public GameObject abilityParent;

    public CameraMoveAnim moveAnim;

    #region Dont touch this
    private static AbilityLoader _instance;
    public static AbilityLoader Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("ability loader Manager is NULL");
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
        
    }

    public void LoadAgentAbilities(Agent agent)
    {
        abilityParent.SetActive(true);
        currentSelectedAgent = agent;

        abilityName1.text = currentSelectedAgent.statistics.ability1.name;
        abilityName2.text = currentSelectedAgent.statistics.ability2.name;
    }

    public void ActivateAbility1()
    {
        ClearTargets();
        FindTargets(currentSelectedAgent.statistics.ability1);
    }

    public void ActivateAbility2()
    {
        ClearTargets();
        FindTargets(currentSelectedAgent.statistics.ability2);
    }

    public void ActivateNullAbility()
    {

    }

    private void FindTargets(Ability ability)
    {

        currentAbility = ability;

        if(ability.isRanged)//if the ability is ranged have the player select their target
        {
            if(ability.isHealingAbility)
            {
                friendlyTargets = CombatManager.Instance.agents;
                foreach (Agent agent in friendlyTargets)
                {
                    agent.canBeTargeted = true;
                }
                return;
            }
            enemyTargets = CombatManager.Instance.enemies;
            foreach (Enemy enemy in enemyTargets)
            {
                enemy.canBeTargeted = true;
            }
            return;
        }

        //if the ability isnt ranged then continue here for melee
        List<Tile> tokensInRange = GridManager.Instance.CheckTilesAroundToken(currentSelectedAgent);

        if(tokensInRange.Count == 0)//if no targets in range
        {
            ClearTargets();
            return;
        }
       

        if(ability.isHealingAbility)//melee healing only for agents
        {
            foreach(Tile tile in tokensInRange)
            {
                if(tile.currentToken.gameObject.GetComponent<Agent>())
                {
                    tile.currentToken.gameObject.GetComponent<Agent>().canBeTargeted = true;
                    friendlyTargets.Add(tile.currentToken.gameObject.GetComponent<Agent>());
                }
            }
            //moveAnim.PressButton();
            return;
        }

        //Melee attack for enemies
        foreach (Tile tile in tokensInRange)
        {
            if (tile.currentToken.gameObject.GetComponent<Enemy>())
            {
                tile.currentToken.gameObject.GetComponent<Enemy>().canBeTargeted = true;
                enemyTargets.Add(tile.currentToken.gameObject.GetComponent<Enemy>());
            }
        }

        //moveAnim.PressButton();
    }

    public void UseAbility(Token token)
    {
        abilityParent.SetActive(false);

        if(currentAbility.isHealingAbility)
        {
            for(int i=0; i < currentAbility.numberOfHits; i++)
            {
                token.gameObject.GetComponent<Agent>().currentHealth += currentAbility.damage * currentSelectedAgent.statistics.attack;
            }
            ClearTargets();
            CombatManager.Instance.EndTurn(CombatManager.Instance.currentInInitiative);
            return;
        }

        for (int i = 0; i < currentAbility.numberOfHits; i++)
        {
            token.gameObject.GetComponent<Enemy>().currentHealth -= currentAbility.damage * currentSelectedAgent.statistics.attack;

            Debug.Log("ATTACK");
        }
        ClearTargets();
        CombatManager.Instance.EndTurn(CombatManager.Instance.currentInInitiative);
    }

    public void ClearTargets()
    {
        //moveAnim.PressButton();
        foreach (Agent agent in friendlyTargets)
        {
            agent.canBeTargeted = false;
        }

        foreach (Enemy enemy in enemyTargets)
        {
            enemy.canBeTargeted = false;
        }
        //friendlyTargets.Clear();
        //enemyTargets.Clear();
    }
}
