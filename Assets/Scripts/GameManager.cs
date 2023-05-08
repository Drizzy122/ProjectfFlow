using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Elements;

    public List<GameObject> unlockedElements;

    public TMP_Text textbox;
    public string elementOBJ;
    VoiceLineManager VLM;
    int tutorialStage = 0;
    bool stageDone = false;

    // Start is called before the first frame update
    void Start()
    {
        VLM = FindObjectOfType<VoiceLineManager>();
        Tutorial();
    }

    // Update is called once per frame
    void Update()
    {
        Element[] list = GameObject.FindObjectsOfType<Element>();
        CheckNewElement(list);
        TutorialCheck(list);
    }

    void CheckNewElement(Element[] list){
        foreach(Element check in list){
            foreach(GameObject element in unlockedElements){
                if(element.GetComponent<Element>().elementType == check.elementType){
                    return;
                }
            }

            unlockedElements.Add(Elements[Elements.FindIndex(x => x.name == elementOBJ)]);
        }
    }

    void TutorialCheck(Element[] list){

        foreach(Element check in list){
            if(check.elementType == elementOBJ && !stageDone){
                stageDone = true;
                Tutorial();
            }
        }
    }

    void Tutorial(){
        switch(tutorialStage){
                case 0:
                    elementOBJ = "Water"; 
                    VLM.TutorialVoiceLine(tutorialStage);
                    tutorialStage = 1;
                    break;

                case 1:
                    elementOBJ = "Rock";
                    VLM.TutorialVoiceLine(tutorialStage);
                    tutorialStage = 2;
                    break;
                case 2:
                    elementOBJ = "asdasd";
                    VLM.TutorialVoiceLine(tutorialStage);
                    tutorialStage = 3;
                    break;

        }
        if(tutorialStage != 3){
            textbox.text = $"Make {elementOBJ}";
            stageDone = false;
        }
        else{
            textbox.text = "";
        }
    }
}
