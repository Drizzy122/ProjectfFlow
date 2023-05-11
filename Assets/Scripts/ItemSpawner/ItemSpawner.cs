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

    public float rate;

    public float period = 0.1f;

    int index = 0;

    GameManager GM;

    public TMP_Text box;

    bool canSpawn = false;

    float time;
    private void Start()
    {
        time = 0;
        beltInSequence = null;
        beltInSequence = FindNextBelt();
        gameObject.name = $"ItemSpawner: {ItemSpawnerID++}";
        //InvokeRepeating("SpawnItem", 0, rate);

        GM = GameObject.FindObjectOfType<GameManager>();
        beltItems = GM.unlockedElements;
    }

    private void Update()
    {
        if (period > rate || canSpawn)
        {
            //print("hi");
            canSpawn = true;
            SpawnItem();
            period = 0;
        }
        period += UnityEngine.Time.deltaTime;
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
            canSpawn = false;
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
        beltItems = GM.unlockedElements;
        if(index >= beltItems.Count)
            index = 0;

        box.text = beltItems[index].GetComponent<Element>().elementType;
    }
}
