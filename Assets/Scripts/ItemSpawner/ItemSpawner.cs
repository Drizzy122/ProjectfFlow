using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using TMPro;
public class ItemSpawner : MonoBehaviour
{
    static int ItemSpawnerID;

    public BeltInfo beltInSequence;
    public List<GameObject> beltItems;

    public int rate;

    int index = 0;

    GameManager GM;

    public TMP_Text box;

    private void Start()
    {
        beltInSequence = null;
        beltInSequence = FindNextBelt();
        gameObject.name = $"ItemSpawner: {ItemSpawnerID++}";
        InvokeRepeating("SpawnItem", 0, rate);

        GM = GameObject.FindObjectOfType<GameManager>();
        beltItems = GM.Elements;
    }
    
   

    void SpawnItem()
    {
        //Debug.Log("attempt");
        if (beltInSequence == null)
        {
            beltInSequence = FindNextBelt();
            //Debug.Log("no belt");
        }
        else if (beltInSequence.IsSpaceTaken())
        {
            //Debug.Log("beltfull");
        }
        else
        {
            //Debug.Log("success");
            Instantiate(beltItems[index], beltInSequence.transform.position, Quaternion.identity);
        }
    }

    BeltInfo FindNextBelt()
    {
        Transform currentTransform = transform;
        var forward = transform.right;
        RaycastHit2D hit = Physics2D.Raycast(currentTransform.position, forward, 1f);


        if (hit.collider != null)
        {
            BeltInfo belt = hit.collider.GetComponent<BeltInfo>();

            if (belt != null)
                return belt;
        }
        return null;
    }

    public void RotateItem(){
        index ++;
        
        if(index >= beltItems.Count)
            index = 0;
        else if(index == 3 || index == 1 || index == 6){
            index ++;
        }
        box.text = beltItems[index].GetComponent<Element>().elementType;
    }
}
