using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaAnimationEnd : MonoBehaviour
{
    public GameObject result;
    public GameObject summonButton;
    public void EndAnimation()
    {
        gameObject.SetActive(false);
        result.SetActive(true);
        summonButton.SetActive(true);
    }
}
