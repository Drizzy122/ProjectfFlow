using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltInfo : MonoBehaviour
{
    Belt ownBelt;
    ItemFuser ownFuser;
    ItemBin ownBin;
    ItemSplitter ownSplitter;
    Vector3 itemPos;

    public BeltItem currentItem;

    int type;

    void Awake()
    {
        ownBelt = GetComponent<Belt>();
        if(ownBelt != null){
            type = 1;
            return;
        }

        ownFuser = GetComponent<ItemFuser>();
        if (ownFuser != null){
            type = 2;
            return;
        }
        ownBin = GetComponent<ItemBin>();
        if (ownBin != null){
            type = 3;
            return;
        }
        ownSplitter = GetComponent<ItemSplitter>();
        if(ownSplitter != null){
            type = 4;
            return;
        }



    }

    public Vector3 GetItemPosition()
    {
        var position = transform.position;
        switch(type){
            case 4:
                if(ownSplitter.beltInSequence1 != null && ownSplitter.beltInSequence2 != null && ownSplitter.index == 1){
                    position += transform.up;
                }else if(ownSplitter.beltInSequence1 == null && ownSplitter.beltInSequence2 != null){
                    position += transform.up;
                }
                break;

            default:
                break;
        }
        return new Vector2(position.x, position.y);

    }

    public void TakeSpace(bool state){
        switch(type){
            case 1:
                ownBelt.isSpaceTaken = state;
                break;

            case 2:
                ownFuser.itemInserting = state;
                break;

            case 3:
                ownBin.isSpaceTaken = state;
                break;
            case 4:
                ownSplitter.isSpaceTaken = state;
                break;
        }
    }

    public bool IsSpaceTaken(){
        switch(type){
            case 1:
                return ownBelt.isSpaceTaken;

            case 2:
                return (ownFuser.fuserItems.Count >= 2 || ownFuser.itemInserting || ownFuser.fused);

            case 3:
                return ownBin.isSpaceTaken;

            case 4:
                return ownSplitter.isSpaceTaken;
                
            default:
            return false;
        }
    }

    public void GiveItem(BeltItem item){
        currentItem = item;
        switch(type){
            case 1:
                ownBelt.beltItem = item;
                item.AssignBelt(gameObject);
                break;

            case 2:
                ownFuser.fuserItems.Add(item);
                item.AssignBelt(gameObject);
                ownFuser.itemInserting = false;
                break;

            case 3:
                ownBin.beltItem = item;
                item.AssignBelt(gameObject);
                break;

            case 4:
                ownSplitter.beltItem = item;
                item.AssignBelt(gameObject);
                break;
        }
    }

}
