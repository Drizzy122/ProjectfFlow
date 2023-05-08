using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineManager : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> TutorialClips;
    AudioSource AS;

    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TutorialVoiceLine(int index){
        print(index);
        AS.clip = TutorialClips[index];
        AS.Play();
        index++;
    }
}
