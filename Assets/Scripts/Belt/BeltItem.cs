using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltItem : MonoBehaviour
{
    public GameObject item;
    public bool onBelt = false;
    public GameObject currentBelt;

    float clock = 0;

    private void Awake()
    {
        item = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(onBelt == false){
            clock += Time.deltaTime;
        }
        else if(currentBelt == null){
            currentBelt = null;
            onBelt = false;
            clock = 0;
        }
        if(clock >= 10f){
            clock = 0;
            print("hi");
            Destroy(gameObject);
        }
    }

    public void AssignBelt(GameObject belt){
        if(belt != null){
            onBelt = true;
            clock = 0;
        }else{
            onBelt = false;
        }
        currentBelt = belt;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BeltInfo belt = collision.gameObject.GetComponent<BeltInfo>();
        if (belt != null && !onBelt)
        {
            if (!belt.IsSpaceTaken())
            {
                belt.TakeSpace(true);
                //print("bruh");
                belt.GiveItem(gameObject.GetComponent<BeltItem>());
            }
        }
    }
}
