using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {
    public float upperx;
    public float uppery;

    public float lowerx;
    public float lowery;

    void Update() {
        if (transform.position.x > upperx) {
            transform.position = new Vector2(upperx, transform.position.y);
        }
        if (transform.position.x > uppery) {
            transform.position = new Vector2(transform.position.x, uppery);
        }
    }
}
