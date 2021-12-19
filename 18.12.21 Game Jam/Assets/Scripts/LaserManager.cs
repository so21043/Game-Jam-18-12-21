using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    public GameObject Laser;
    public float timer;
    public float timerOrig;
    private int bulletsShot = 0;
    private int bullets;
    public float shootTimer;
    public float shootTimerOrig;

    private bool shoot = false;

    // Start is called before the first frame update
    void Start() {
        timer = timerOrig;
    }

    // Update is called once per frame
    void Update() {
        if (shoot == false) {
            if (timer > 0) {
                timer -= Time.deltaTime;
            } else {
                shoot = true;
            }
        }

        if (shoot == true) {
            shootTimer -= Time.deltaTime;

            if (shootTimer <= 0) {
                GameObject newLaser = Instantiate(Laser, transform.position, transform.rotation);
                newLaser.GetComponent<Rigidbody2D>().velocity = transform.up * 10f;
                bulletsShot += 1;
                shootTimer = shootTimerOrig;
            }

            if (bulletsShot == 5) {
                timer = timerOrig;
                bulletsShot = 0;
                shoot = false;
            }
        }
    }
}
