using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineManager : MonoBehaviour
{
    public List<AudioClip> TutorialClips;

    public List<AudioClip> explosionVoiclines;
    
    public List<AudioClip> randomVoiceLines;
    int EVLIndex = 0;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TutorialVoiceLine(int index){
        audioSource = GetComponent<AudioSource>();
        print(index);
        audioSource.clip = TutorialClips[index];
        audioSource.Play();
        index++;
    }

    public void PlayClip(AudioClip clip){
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void VoiceLine(string type){
        audioSource = GetComponent<AudioSource>();
        switch(type){
            case "Explosion":
                if(EVLIndex >= 10)
                    return;
                audioSource.clip = explosionVoiclines[EVLIndex];
                audioSource.Play();
                EVLIndex++;
                break;
        }

    }

    void RandomVoiceLine(){

    }
}
