using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Elements;

    public List<int> ElementWorth;

    public List<GameObject> unlockedElements;

    public TMP_Text textbox;
    public string elementOBJ;
    VoiceLineManager VLM;
    int tutorialStage = 0;
    bool stageDone = false;
    public int money;

    public TMP_Text moneyText;

    public List<AudioClip> explosionVoiceLines;
    int explosionVLIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        VLM = FindObjectOfType<VoiceLineManager>();
        Tutorial();
        money = PlayerPrefs.GetInt("Money",0);
        moneyText.text = money.ToString();
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

            VLM.VoiceLine(check.elementType);
            if(check.elementType == "Explosion"){
                return;
            }
            print(Elements.FindIndex(x => x.name == check.elementType));
            unlockedElements.Add(Elements[Elements.FindIndex(x => x.name == check.elementType)]);
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
                    elementOBJ = "ghfu";
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

    public void AddMoney(int amount){
        money += amount;
        print(amount);
        moneyText.text = money.ToString();
    }

    public void DelMoney(int amount){
        money -= amount;
        print(-amount);
        moneyText.text = money.ToString();
    }

    void OnApplicationQuit(){
        PlayerPrefs.SetInt("Money", money);
        Debug.Log("Application ending after " + Time.time + " seconds");
    }
}
