using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemBin : MonoBehaviour
{
    static int beltID;
    public BeltItem beltItem;
    public bool isSpaceTaken;
    public float cooldownTime = 1f;
    bool isDestroying = false;

    GameManager GM;

    public AudioClip sellSound;

    AudioSource AS;

    private void Start()
    {
        AS = GetComponent<AudioSource>();
        AS.clip = sellSound;
        GM = FindObjectOfType<GameManager>();
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
        GameObject el = GM.Elements.SingleOrDefault(x => x.name == beltItem.GetComponent<Element>().elementType);
        
        //print(GM.Elements.IndexOf(el) + " " + beltItem.GetComponent<Element>().elementType);
        
        int amount = GM.ElementWorth[GM.Elements.IndexOf(el)];
        GM.AddMoney(amount);

        Destroy(beltItem.gameObject);
        AS.Play();
        beltItem = null;

        isSpaceTaken = false;
        isDestroying = false;
        
    }
}
