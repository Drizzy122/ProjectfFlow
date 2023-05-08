using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exposion : MonoBehaviour
{
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
        print(collider);
        collider.isTrigger = true;
        collider.radius = radius;

        Invoke("DestroySelf", 1);

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
