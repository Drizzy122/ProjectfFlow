using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public List<GameObject> Elements;

    public TMP_Text textbox;
    public string elementOBJ;

    // Start is called before the first frame update
    void Start()
    {
        elementOBJ = Elements[Random.Range(0,Elements.Count-1)].GetComponent<Element>().elementType;
        textbox.text = $"Make {elementOBJ}";
    }

    // Update is called once per frame
    void Update()
    {
        Element[] list = GameObject.FindObjectsOfType<Element>();
        foreach(Element check in list){
            if (check.elementType == elementOBJ){
                elementOBJ = Elements[Random.Range(0,Elements.Count-1)].GetComponent<Element>().elementType;
                textbox.text = $"Make {elementOBJ}";
            }
        }
    }
}
