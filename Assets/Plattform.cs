using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plattform : MonoBehaviour {
    
    public float amplitude;

    Vector2 startPosition;

    // Start is called before the first frame update
    void Start() {
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update() {
        transform.position = startPosition + (Vector2.right * Mathf.Sin(Time.time) * amplitude);
    }

    // Here we should check if it is the player we collided with.
    void OnCollisionEnter2D(Collision2D other) {
        other.transform.parent = transform;
    }
 
    // Here we should check if it is the player we collided with.
    void OnCollisionExit2D(Collision2D other) {
        other.transform.parent = null; 
    }
}
