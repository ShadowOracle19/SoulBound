using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeployAgent : MonoBehaviour,IDragHandler,IDropHandler, IBeginDragHandler
{
    public Vector3 startPosition;
    public GameObject agentPrefab;
    public bool agentSpawned = false;
    public bool deploying = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        deploying = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        GridManager.Instance.dragToDeployAgent = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GetComponent<RectTransform>().localPosition = Vector3.zero;
        GridManager.Instance.deployingAgent = null;
        GridManager.Instance.dragToDeployAgent = false;
        GridManager.Instance.deployAgentOverTile = false;
        deploying = false;

    }

    public void SpawnAgent()
    {
        if(agentSpawned) return;
        agentSpawned = true;
        GridManager.Instance.deployingAgent = Instantiate(agentPrefab, new Vector3(100, 100), Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(GridManager.Instance.dragToDeployAgent && GridManager.Instance.deployAgentOverTile && deploying)
        {
            SpawnAgent();
            agentSpawned = true;
        }
        if (agentSpawned)
        {
            GetComponent<Image>().color = Color.clear;
        }
    }

   
}
