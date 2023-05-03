using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBelt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Vector3 MousePos()
    {
        Vector2 ScreenPosition = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(ScreenPosition);

        return worldPos;
    }

    GameObject GetGameObject()
    {
        Vector3 mousePos = MousePos();
        Collider2D hit = Physics2D.OverlapPoint(mousePos);
        //print(hit);
        if (hit != null)
        {
            return hit.gameObject;
        }
        return null;
    }
}
