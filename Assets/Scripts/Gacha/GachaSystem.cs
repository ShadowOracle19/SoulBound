using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaSystem : MonoBehaviour
{
    public List<gatchaPosibilities> agents = new List<gatchaPosibilities>();
    public List<gatchaPosibilities> nulls = new List<gatchaPosibilities>();

    public GameObject gachaAnimation;

    public GachaResult gachaResult;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PullAgent()
    {
        int randNum = Random.Range(0, agents.Count);
        gachaResult.image.sprite = agents[randNum].sprite;
        gachaResult.pullName.text = agents[randNum].name;
    }

    public void PullNull()
    {
        int randNum = Random.Range(0, nulls.Count);
        gachaResult.image.sprite = nulls[randNum].sprite;
        gachaResult.pullName.text = nulls[randNum].name;

    }
}

[System.Serializable]
public struct gatchaPosibilities
{
    public string name;
    public Sprite sprite;
}