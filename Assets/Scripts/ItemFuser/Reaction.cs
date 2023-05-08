using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Reaction : MonoBehaviour
{
    public List<string> elementNames;
    public List<GameObject> elementGO;

    ItemFuser fuser;

    GameManager GM;

    GameObject produce;

    public Dictionary<string, Dictionary<string, string>> elementMap = new Dictionary<string, Dictionary<string, string>>()
        {
            {"Water", new Dictionary<string, string>()
                {
                    {"Water", "Water"},
                    {"Lava", "Rock"},
                    {"Hydrochloric Acid", "Explosion"},
                    {"Ice","Ice"},
                    {"Rock","Rock"},
                    {"Frozen Acid", "Frozen Acid"},
                    {"Steam", "Water"},
                    {"Hydrochloric gas", "Hydrochloric Acid"}
                }
            },
            {"Lava", new Dictionary<string, string>()
                {
                    {"Lava","Lava"},
                    {"Hydrochloric Acid" ,"Lava"},
                    {"Ice", "Steam"},
                    {"Rock", "Lava"},
                    {"Frozen Acid", "Explosion"},
                    {"Steam", "Hydrochloric Gas"},
                    {"Hydrochloric Gas", "Lava"}
                }
            },
            {"Hydrochloric Acid", new Dictionary<string, string>()
                {
                    {"Hydrochloric Acid", "Hydrochloric Acid"},
                    {"Ice", "Explosion"},
                    {"Rock", "Lava"},
                    {"Frozen Acid", "Hydrochloric Acid"},
                    {"Steam", "Hydrochloric Acid"},
                    {"Hydrochloric Gas", "Hydrochloric Acid"}
                }

            },
            {"Ice" , new Dictionary<string, string>()
                {
                    {"Ice", "Ice"},
                    {"Rock", "Rock"},
                    {"Frozen Acid", "Ice"},
                    {"Steam", "Water"},
                    {"Hydrochloric Gas", "Explosion"}

                }
            },
            {"Rock", new Dictionary<string, string>()
                {
                    {"Rock","Rock"},
                    {"Frozen Acid", "Rock"},
                    {"Steam", "Lava"},
                    {"Hydrochloric Gas", "Hydrochloric Gas"}
                    
                }
            },
            {"Frozen Acid", new Dictionary<string, string>()
                {
                    {"Frozen Acid", "Frozen Acid"},
                    {"Steam", "Hydrochloric Acid"},
                    {"Hydrochloric Gas", "Hydrochloric Acid"}
                }
            },
            {"Steam", new Dictionary<string, string>()
                {
                    {"Steam", "Steam"},
                    {"Hydrochloric Gas", "Explosion"}
                }
            },
            {"Hydrochloric Gas", new Dictionary<string, string>()
                {
                    {"Hydrochloric Gas", "Hydrochloric Gas"}
                }
            }
        };



    void Start(){
        GM = GameObject.FindObjectOfType<GameManager>();
        fuser = GetComponent<ItemFuser>();
    }

    public GameObject React(GameObject item1, GameObject item2){
        string element1 = item1.GetComponent<Element>().elementType;
        string element2 = item2.GetComponent<Element>().elementType;

        if (elementMap.ContainsKey(element1) && elementMap[element1].ContainsKey(element2))
        {
            print("Option1");
            produce = GM.Elements.SingleOrDefault(x => x.name == elementMap[element1][element2]);
        }else if(elementMap.ContainsKey(element2) && elementMap[element2].ContainsKey(element1)){
            print("option2");
            produce = GM.Elements.SingleOrDefault(x => x.name == elementMap[element2][element1]);
        }
        
        return InstantiateOBJ();
    }

    GameObject InstantiateOBJ(){
        return Instantiate(produce,fuser.GetItemPosition(), Quaternion.identity);
    }
}
