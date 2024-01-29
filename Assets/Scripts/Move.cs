using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject selectedObject;
    public GameObject target;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                target.transform.position = targetObject.transform.transform.position;
                //offset = selectedObject.transform.position - mousePosition;
            }
        }
        if(selectedObject)
        {

        }
        //if (selectedObject)
        //{
        //    selectedObject.transform.position = mousePosition + offset;
        //}
        //if (Input.GetMouseButtonUp(0) && selectedObject)
        //{
        //    selectedObject = null;
        //}
    }
}
