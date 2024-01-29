using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;


    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseDown()
    {
        if(this.tag == "Grid")
        {
            Debug.Log(gameObject.name);
            GridManager.Instance.target.transform.position = new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
        }
        else
        {
            Debug.Log(gameObject.name);
        }
           
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }
}
