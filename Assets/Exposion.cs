using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exposion : MonoBehaviour
{
    public float radius;
    public AudioClip explosionSound;
    AudioSource AS;

    VoiceLineManager VLM;
    // Start is called before the first frame update
    void Start()
    {
        VLM = FindObjectOfType<VoiceLineManager>();
        AS = GetComponent<AudioSource>();
        AS.clip = explosionSound;
        CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
        print(collider);
        collider.isTrigger = true;
        collider.radius = radius;

        Invoke("DestroySelf", 1);
        AS.Play();
        VLM.VoiceLine("Explosion");
    }

    void DestroySelf(){
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        print(other.name);
        Destroy(other.gameObject);
    }
}
