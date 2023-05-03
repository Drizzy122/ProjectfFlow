using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFuser : MonoBehaviour
{
    static int fuserID;

    public BeltInfo beltInSequence;
    public List<BeltItem> fuserItems;
    public BeltItem produce;

    public bool itemInserting;
    public bool fused;

    public float produceTime = 1f;

    BeltManager BM;

    bool createPoduce;

    Reaction react;

    [Header("TEMP")]
    public BeltItem tempItem;

    private void Start()
    {
        BM = FindObjectOfType<BeltManager>();
        beltInSequence = null;
        beltInSequence = FindNextBelt();
        gameObject.name = $"ItemFuser: {fuserID++}";

        react = gameObject.GetComponent<Reaction>();
    }
    private void Update()
    {
        if (beltInSequence == null)
        {
            beltInSequence = FindNextBelt();
        }
        if (produce != null && produce.item != null && fused)
        {
            StartCoroutine(StartBeltMove());
        }

        if(fuserItems.Count == 2 && !createPoduce){
            createPoduce = true;
            Invoke("Produce",produceTime);
        }
    }

    void Produce(){

        //yield return new WaitForSeconds(produceTime);
        GameObject produceGM = react.React(fuserItems[0].gameObject, fuserItems[1].gameObject);
        produce = produceGM.GetComponent<BeltItem>();

        produce.currentBelt = gameObject;
        produce.onBelt = true;
        
        while(fuserItems.Count != 0){
            if(fuserItems[0] != null){
                Destroy(fuserItems[0].gameObject);
            }
            fuserItems.RemoveAt(0);
        }
        fuserItems.Clear();
        fused = true;
        createPoduce = false;
    }

    public Vector3 GetItemPosition()
    {
        var position = transform.position;

        return new Vector2(position.x, position.y);
    }

    IEnumerator StartBeltMove()
    {
        itemInserting = true;

        if(IsBeltMoveValid())
        {
            Vector3 toPosition = beltInSequence.GetItemPosition();

            beltInSequence.TakeSpace(true);

            var step = BM.speed * Time.deltaTime;

            while(produce.item.transform.position != toPosition)
            {
                produce.item.transform.position =
                    Vector3.MoveTowards(produce.transform.position, toPosition, step);

                yield return null;
            }


            itemInserting = false;
            beltInSequence.GiveItem(produce);
            produce = null;
            fused = false;
        }
    }

    bool IsBeltMoveValid()
    {
        if (produce.item == null)
            return false;
        if (beltInSequence == null)
            return false;
        if (beltInSequence.IsSpaceTaken())
            return false;

        return true;
    }

    BeltInfo FindNextBelt()
    {
        Transform currentBeltTransform = transform;
        var forward = transform.right;
        RaycastHit2D hit = Physics2D.Raycast(currentBeltTransform.position, forward, 1f);

        if (hit.collider != null)
        {
            BeltInfo belt = hit.collider.GetComponent<BeltInfo>();

            if(belt != null)
                return belt;
        }
        return null;
    }
}
