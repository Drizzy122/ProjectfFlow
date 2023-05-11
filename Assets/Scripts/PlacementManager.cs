using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlacementManager : MonoBehaviour
{
    public List<GameObject> Items;
    public List<Sprite> Sprites;
    public List<int> ItemPrice;
    public GameObject IMGPrefab;
    GameObject belts;
    GameObject UIGrid;
    Quaternion rotation;
    [SerializeField] int index = 0;
    List<int> indexes = new List<int>();
    List<GameObject> inventoryItems = new List<GameObject>();
    int rot = 0;
    GameManager GM;

    public List<GameObject> Rotatable;

    public AudioClip itemPlaceSound;
    public AudioClip itemDestroyedSound;
    public AudioClip select;

    public AudioClip itemSplitterAudio;
    public AudioClip removerAudio;
    bool removerBool = false;

    AudioSource AS;


    private void Start()
    {
        AS = GetComponent<AudioSource>();
        belts = GameObject.Find("Belts");
        PopulateInventory();
        GM = GameObject.FindObjectOfType<GameManager>();
    }

    private void Update()
    {
    }

    public void PlaceItem(InputAction.CallbackContext context)
    {
        if (context.performed && ValidLocation() && !MouseOverUI() && index != Items.Count-1)
        {
            if(ItemPrice[index] > GM.money){
                return;
            }
            Vector3 pos = GridPosition();
            GM.DelMoney(ItemPrice[index]);

            GameObject obj =  Instantiate(Items[index], pos, rotation, belts.transform);
            PlaySound(itemPlaceSound);

            switch(Items[index].name){
                case "Splitter":
                    PlaySound(itemSplitterAudio);
                    break;
            }
            foreach(GameObject GO in Rotatable){
                if(Items[index] == GO){
                    return;
                }
            }
            for(int j = 0; j < obj.transform.childCount; j++){
                if(obj.transform.GetChild(j).name != "direction")
                    obj.transform.GetChild(j).rotation = Quaternion.identity;
            }

        }else if(context.performed && !MouseOverUI() && index == Items.Count-1){
            GameObject GO = GetGameObject();
            if(GO != null){
                Destroy(GO);
                GM.AddMoney(ItemPrice[index]);
                PlaySound(itemDestroyedSound);
            }
        }else if(context.performed && !MouseOverUI() && GetGameObject().GetComponent<ItemSpawner>() != null ){
            ItemSpawner spawner = GetGameObject().GetComponent<ItemSpawner>();
            spawner.RotateItem(); 
            PlaySound(select);
        }
    }

    void PlaySound(AudioClip clip){
        AS.clip = clip;
        AS.Play();
    }

    public void RotateItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int rotAmount = 90;
            if(context.control == Keyboard.current.eKey){
                rotAmount *= -1;
            }

            rot += rotAmount;
            rotation = Quaternion.Euler(0, 0, rot);


            UIGrid.transform.GetChild(0).Find("IMG").Rotate(Vector3.forward * rotAmount);
            PlaySound(select);
            
            //Debug.Log(rotation.eulerAngles);

            //rotation = quaternion;
        }
        
    }


    Vector3 MousePos()
    {

        Vector2 ScreenPosition = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(ScreenPosition);

        return worldPos;
    }

    Vector3 GridPosition()
    {
        Vector3 pos = MousePos();
        //Debug.Log(pos);
        pos.y = Mathf.Round(pos.y);
        pos.x = Mathf.Round(pos.x);
        pos.z = 0;
        //Debug.Log(pos);
        return pos;
    }

    bool ValidLocation()
    {
        Vector3 mousePos = MousePos();
        Collider2D hit = Physics2D.OverlapPoint(mousePos);
        //print(hit);
        if (hit != null)
        {
            return false;
        }
        return true;
    }

    GameObject GetGameObject(){
        Vector3 mousePos = MousePos();
        Collider2D hit = Physics2D.OverlapPoint(mousePos);
        //print(hit);
        if (hit != null)
        {
            return hit.gameObject;
        }
        return null;
    }

    public void indexChanger(int I)
    {
        index = I;

        if(removerBool && index == Items.Count-1){
            removerBool = true;
            PlaySound(removerAudio);
        }
    }


    private bool MouseOverUI()
    {
        //print(EventSystem.current.IsPointerOverGameObject());
        return EventSystem.current.IsPointerOverGameObject();
    }

    void PopulateInventory()
    {
        int i = 0;
        UIGrid = GameObject.Find("UI Grid");
        foreach(Sprite sprite in Sprites)
        {
            indexes.Add(i);
            GameObject inventoryDisplay = Instantiate(IMGPrefab, UIGrid.transform);
            inventoryDisplay.transform.Find("IMG").GetComponent<Image>().sprite = sprite;
            int x = i;
            inventoryDisplay.transform.Find("IMG").GetComponent<Button>().onClick.AddListener(() => indexChanger(x));
            inventoryItems.Add(inventoryDisplay);
            inventoryDisplay.transform.Find("Text").gameObject.GetComponent<TMP_Text>().text = sprite.name;
            i++;
        }
    }
}
