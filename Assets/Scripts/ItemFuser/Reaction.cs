using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Reaction : MonoBehaviour
{
    public List<string> elementNames;
    public List<GameObject> elementGO;

    public AudioClip frozenAcid;
    bool frozenAcidDone = false;

    public AudioClip hydrochloricAcid;
    bool hydrochloricAcidDone = false;

    public AudioClip steam;
    bool steamDone = false;

    AudioSource AS;

    ItemFuser fuser;

    GameManager GM;

    GameObject produce;

    public Dictionary<string, Dictionary<string, string>> elementMap = new Dictionary<string, Dictionary<string, string>>()
        {
            {"Water", new Dictionary<string, string>()
                {
                    {"Water", "Water"},
                    {"Lava", "Rock"},
                    {"Hydrochloric Acid", "Steam"},
                    {"Ice","Ice"},
                    {"Rock","Rock"},
                    {"Frozen Acid", "Explosion"},
                    {"Steam", "Steam"},
                    {"Hydrochloric gas", "Hydrochloric Acid"}
                }
            },
            {"Lava", new Dictionary<string, string>()
                {
                    {"Lava","Lava"},
                    {"Hydrochloric Acid" ,"Explosion"},
                    {"Ice", "Hydrochloric Acid"},
                    {"Rock", "Explosion"},
                    {"Frozen Acid", "Explosion"},
                    {"Steam", "Hydrochloric Gas"},
                    {"Hydrochloric Gas", "Explosion "}
                }
            },
            {"Hydrochloric Acid", new Dictionary<string, string>()
                {
                    {"Hydrochloric Acid", "Hydrochloric Acid"},
                    {"Ice", "Explosion"},
                    {"Rock", "Explosion"},
                    {"Frozen Acid", "Hydrochloric Acid"},
                    {"Steam", "Hydrochloric Gas"},
                    {"Hydrochloric Gas", "Hydrochloric Acid"}
                }

            },
            {"Ice" , new Dictionary<string, string>()
                {
                    {"Ice", "Ice"},
                    {"Rock", "Explosion"},
                    {"Frozen Acid", "Ice"},
                    {"Steam", "Water"},
                    {"Hydrochloric Gas", "Hydrochloric Acid"}

                }
            },
            {"Rock", new Dictionary<string, string>()
                {
                    {"Rock","Rock"},
                    {"Frozen Acid", "Explosion"},
                    {"Steam", "Lava"},
                    {"Hydrochloric Gas", "Explosion"}
                    
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
        AS = GetComponent<AudioSource>();
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
            PlaySound(elementMap[element1][element2]);
        }else if(elementMap.ContainsKey(element2) && elementMap[element2].ContainsKey(element1)){
            print("option2");
            produce = GM.Elements.SingleOrDefault(x => x.name == elementMap[element2][element1]);
            PlaySound(elementMap[element2][element1]);
        }
        
        return InstantiateOBJ();
    }

    void PlaySound(string sound){
        switch(sound){
            case "Steam":
                if(!steamDone){
                    AS.clip = steam;
                    AS.Play();
                    steamDone = true;
                }
                break;

            case "Hydrochloric Acid":
                if(!hydrochloricAcidDone){
                    AS.clip = hydrochloricAcid;
                    AS.Play();
                    hydrochloricAcidDone = true;
                }
                break;

            case "Frozen Acid":
                if(!frozenAcidDone){
                    AS.clip = frozenAcid;
                    AS.Play();
                    frozenAcidDone = true;
                }
                break;
            default:
            break;
        }
    }

    GameObject InstantiateOBJ(){
        return Instantiate(produce,fuser.GetItemPosition(), Quaternion.identity);
    }
}
