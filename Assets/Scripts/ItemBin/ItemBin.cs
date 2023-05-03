using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBin : MonoBehaviour
{
    static int beltID;
    public BeltItem beltItem;
    public bool isSpaceTaken;
    public float cooldownTime = 1f;
    bool isDestroying = false;

    private void Start()
    {
        beltItem = null;
        gameObject.name = $"ItemBin: {beltID++}";
    }
    private void Update()
    {
        if(beltItem != null && beltItem.item != null && isSpaceTaken && !isDestroying){
            isDestroying = true;
            Invoke("StartDestroy",cooldownTime);
        }
    }

    void StartDestroy()
{
        isSpaceTaken = true;

        Destroy(beltItem.gameObject);

        beltItem = null;

        isSpaceTaken = false;
        isDestroying = false;
        
    }
}
