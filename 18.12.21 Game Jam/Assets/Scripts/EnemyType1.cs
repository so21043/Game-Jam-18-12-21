using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : MonoBehaviour
{
    private Rigidbody2D rb;

    public float patrolDistanceOrig;
    [SerializeField] private float patrolDistance;
    public float speed;
    public float chaseSpeed;

    public bool chasing = false;
    private Vector2 movement;

    public GameObject player;
    public float lookDistance;

    public int type;


    // Start is called before the first frame update
    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        patrolDistance = patrolDistanceOrig;
        chaseSpeed = speed;
        player = GameObject.FindWithTag("Player");

        Physics2D.queriesStartInColliders = false;
    }

    void Update() {
        /*float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);*/

        // checking if player is seen using raycasts
        //Debug.DrawLine(new Vector2(200,200), Vector3.zero, Color.green, 2, false);
        if (type == 1) {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, lookDistance);

            if (hitInfo.collider != null) {
                //Debug.DrawLine(transform.position, hitInfo.point, Color.red);
                //chasing = true;
                if (hitInfo.collider.CompareTag("Player")) {
                    //Debug.DrawLine(transform.position, hitInfo.point, Color.blue);
                    chasing = true;
                }

            }
        }
    }

    void FixedUpdate() {
        if (type == 1) {
            if (chasing == false) {
                patrol();
            }
            if (chasing == true) {
                chase();
            }    
        }

        if (type == 0) {
            patrolNormal();
        }
    }

    void patrol() {
        if (patrolDistance > 0) {
            patrolDistance -= Mathf.Abs(speed);
        } else {
            speed = -speed;
            patrolDistance = patrolDistanceOrig;
        }

        movement = new Vector2(speed, 0f);

        // flipping sprite
        if (speed > 0) {
            transform.rotation = new Quaternion(0f,0f,0f,0f);
        }
        if (speed < 0) {
            transform.rotation = new Quaternion(0f,0f,180f,0f);
        }

        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    void patrolNormal() {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, 1f);

        if (hitInfo.collider != null) {
            if (hitInfo.collider.CompareTag("Wall")) {
                //Debug.DrawLine(transform.position, hitInfo.point, Color.blue);
                speed = -speed;
            }
        }

        movement = new Vector2(speed, 0f);

        // flipping sprite
        if (speed > 0) {
            transform.rotation = new Quaternion(0f,0f,0f,0f);
        }
        if (speed < 0) {
            transform.rotation = new Quaternion(0f,0f,180f,0f);
        }

        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    void chase() {
        // chasing player
        

        if (player.transform.position.x > transform.position.x) {
            movement = new Vector2(chaseSpeed, 0f);
            transform.rotation = new Quaternion(0f,0f,0f,0f);
        }
        if (player.transform.position.x < transform.position.x) {
            movement = new Vector2(-chaseSpeed, 0f);
            transform.rotation = new Quaternion(0f,0f,180f,0f);
        }
        if (player.transform.position.x == transform.position.x)  {
            movement = new Vector2(0f,0f);
        }

        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }
}
