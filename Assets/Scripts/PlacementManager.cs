using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;

public class PlacementManager : MonoBehaviour
{
    public List<GameObject> Items;
    public List<Sprite> Sprites;
    
    public GameObject IMGPrefab;

    GameObject belts;
    GameObject UIGrid;
    Quaternion rotation;
    [SerializeField] int index = 0;
    List<int> indexes = new List<int>();
    List<GameObject> inventoryItems = new List<GameObject>();
    int rot = 0;

    GameManager GM;

    private void Start()
    {
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
            Vector3 pos = GridPosition();
            Instantiate(Items[index], pos, rotation, belts.transform);

        }else if(context.performed && !MouseOverUI() && index == Items.Count-1){
            GameObject GO = GetGameObject();
            if(GO != null){
                Destroy(GO);
            }
        }else if(context.performed && !MouseOverUI() && GetGameObject().GetComponent<ItemSpawner>() != null ){
            ItemSpawner spawner = GetGameObject().GetComponent<ItemSpawner>();
            spawner.RotateItem();
        }
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

            foreach(GameObject GM in inventoryItems){
                GM.transform.Find("IMG").Rotate(Vector3.forward * rotAmount);
            }
            //Debug.Log(rotation.eulerAngles);

            //rotation = quaternion;
        }
        
    }

    void SelectItem(int index)
    {

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
