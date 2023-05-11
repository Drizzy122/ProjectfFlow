using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
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
    float brokeTimer = 0;

    public GameObject GameOverText;

    int explosionVLIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        VLM = FindObjectOfType<VoiceLineManager>();
        Tutorial();
        moneyText.text = money.ToString();
        brokeTimer = 0;
        GameOverText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Element[] list = GameObject.FindObjectsOfType<Element>();
        CheckNewElement(list);
        TutorialCheck(list);

        if(money <= 1){
            brokeTimer += Time.deltaTime;
            if(brokeTimer >= 5){
                GameOverText.SetActive(true);
                Invoke("MainMenu",5);
            }
        }else{
            brokeTimer = 0;
        }
    }

    void CheckNewElement(Element[] list){
        foreach(Element check in list){
            foreach(GameObject element in unlockedElements){
                if(element.GetComponent<Element>().elementType == check.elementType){
                    return;
                }
            }

            
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
        //print(amount);
        moneyText.text = money.ToString();
    }

    public void DelMoney(int amount){
        money -= amount;
        //print(-amount);
        moneyText.text = money.ToString();
    }

    void OnApplicationQuit(){
        PlayerPrefs.DeleteAll();
        Debug.Log("Application ending after " + Time.time + " seconds");
    }

    void MainMenu(){
        SceneManager.LoadScene(0);
    }

}
