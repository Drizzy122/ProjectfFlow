using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    static int beltID;

    public BeltInfo beltInSequence;
    public BeltItem beltItem;
    public bool isSpaceTaken;

    BeltManager BM;

    Coroutine coroutine;

    private void Start()
    {

        BM = FindObjectOfType<BeltManager>();
        beltInSequence = null;
        beltItem = null;
        beltInSequence = FindNextBelt();
        gameObject.name = $"Belt: {beltID++}";
    }
    private void Update()
    {
        if (beltInSequence == null){
            beltInSequence = FindNextBelt();
        }
        if(beltItem != null && beltItem.item != null && isSpaceTaken){
            if(beltInSequence != null)
                BeltMove();
        }
        if(beltItem == null && coroutine != null){
            StopAllCoroutines();
            coroutine = null;
        }

    }

    void BeltMove(){
        coroutine = StartCoroutine(StartBeltMove());
    }


    IEnumerator StartBeltMove()
    {
        isSpaceTaken = true;

        if(IsBeltMoveValid())
        {
            Vector3 toPosition = beltInSequence.GetItemPosition();

            beltInSequence.TakeSpace(true);

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
            
            if(beltInSequence != null){
                beltInSequence.GiveItem(beltItem);
            }
            beltItem = null;
            coroutine = null;
        }
    }

    bool IsBeltMoveValid()
    {
        if (beltItem.item == null)
            return false;
        if (beltInSequence == null)
            return false;
        if (beltInSequence.IsSpaceTaken())
            return false;
        if(beltItem == null)
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
            
            if(belt != null){
                return belt;
            }
            
        }
        return null;
    }
}
