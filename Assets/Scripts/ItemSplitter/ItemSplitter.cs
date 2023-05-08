using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSplitter : MonoBehaviour
{
    static int beltID;

    public BeltInfo beltInSequence1;
    public BeltInfo beltInSequence2;

    public BeltItem beltItem;
    public bool isSpaceTaken;

    BeltManager BM;

    Coroutine coroutine;

    public int index = 1;

    private void Start()
    {

        BM = FindObjectOfType<BeltManager>();
        beltInSequence1 = null;
        beltInSequence2 = null;
        beltItem = null;
        beltInSequence1 = FindNextBelts(0);
        beltInSequence2 = FindNextBelts(1);
        gameObject.name = $"ItemSplitter: {beltID++}";

        if(beltInSequence1 != null && beltInSequence2 == null){
            index = 1;
        }else if (beltInSequence1 == null && beltInSequence2 != null){
            index = -1;
        }
    }
    private void Update()
    {
        if (beltInSequence1 == null){
            beltInSequence1 = FindNextBelts(0);
        }
        if(beltInSequence2 == null){
            beltInSequence2 = FindNextBelts(1);
        }
        if(beltItem != null && beltItem.item != null && isSpaceTaken && coroutine == null){
            if(beltInSequence1 != null || beltInSequence2 != null)
                coroutine = StartCoroutine(StartBeltMove());
        }
        if(beltItem == null && coroutine != null){
            StopAllCoroutines();
            coroutine = null;
        }

    }

    IEnumerator StartBeltMove()
    {
        Vector3 toPosition = new Vector3(0,0,0);
        if(beltInSequence1 != null && beltInSequence2 != null){
            index *= -1;
        }else if(beltInSequence1 != null && beltInSequence2 == null){
            index = 1;
        }else if (beltInSequence1 == null && beltInSequence2 != null){
            index = -1;
        }else{
            StopCoroutine(coroutine);
        }

        isSpaceTaken = true;

        if(IsBeltMoveValid())
        {

            switch(index){
                case 1:
                    toPosition = beltInSequence1.GetItemPosition();
                    beltInSequence1.TakeSpace(true);
                    break;

                case -1:
                    toPosition = beltInSequence2.GetItemPosition();
                    beltInSequence2.TakeSpace(true);
                    break;
            }
            var step = BM.speed * Time.deltaTime;

            while(beltItem != null && beltItem.item != null && beltItem.item.transform.position != toPosition)
            {
                if(beltItem == null || beltItem.item == null){
                    isSpaceTaken = false;
                }
                beltItem.item.transform.position = Vector3.MoveTowards(beltItem.transform.position, toPosition, step);
                yield return null;
            }

            isSpaceTaken = false;
            
            if(index == 1 && beltInSequence1 != null){
                beltInSequence1.GiveItem(beltItem);
            }else if(index == -1 && beltInSequence2 != null){
                beltInSequence2.GiveItem(beltItem);
            }
            beltItem = null;
            coroutine = null;
        }
    }

    bool IsBeltMoveValid()
    {
        switch(index){

            case 1:
                if (beltInSequence1 == null)
                    return false;
                if (beltInSequence1.IsSpaceTaken())
                    return false;
                break;
            case -1:
                if (beltInSequence2 == null)
                    return false;
                if (beltInSequence2.IsSpaceTaken())
                    return false;
                break; 
        }

        if (beltItem.item == null)
            return false;
        if(beltItem == null)
            return false;
        
        return true;
    }

    BeltInfo FindNextBelts(int pos)
    {
        Transform currentBeltTransform = transform;
        var forward = transform.right;
        Vector3 position = currentBeltTransform.position + (transform.up * pos);
        RaycastHit2D hit = Physics2D.Raycast(position, forward, 1f);


        if (hit.collider != null)
        {
            BeltInfo belt = hit.collider.GetComponent<BeltInfo>();
            
            if(belt != null){
                return belt;
            }
            
        }
        return null;
    }
}
