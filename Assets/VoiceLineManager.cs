using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineManager : MonoBehaviour
{
    public List<AudioClip> TutorialClips;
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
}
