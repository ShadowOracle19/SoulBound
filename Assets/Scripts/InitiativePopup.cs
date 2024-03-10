using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InitiativePopup : MonoBehaviour
{
    public Token connectedToken;

    public TextMeshProUGUI tokenName;
    public Image tokenSprite;

    public GameObject currentTurnVisual;

    public bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(connectedToken.currentTurn)
        {
            currentTurnVisual.SetActive(true);
        }
        else
        {
            currentTurnVisual.SetActive(false);
        }

        if(connectedToken.IsDestroyed())
        {
            Destroy(gameObject);
        }
    }
}
