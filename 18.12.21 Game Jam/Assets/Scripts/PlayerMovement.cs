using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 100f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    private string sceneName;
    private bool dead = false;
    public GameObject CanvasObject;

    // Start is called before the first frame update
    void Start () {
        sceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update () {
        if (dead == false) {
            movementScript();
        }

        if (dead == true) {
            CanvasObject.SetActive(true);
        }

        //Debug.DrawLine(transform.position, transform.position + transform.up * 3f, Color.green);
    }

    public void movementScript() {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            if (crouch == false) {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
        }

        animator.SetBool("IsJumping", !GetComponent<CharacterController2D>().m_Grounded);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, 3f);

        if (Input.GetButton("Crouch"))
        {
            crouch = true;
            Debug.Log("Crouching");
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            if (hitInfo.collider == null) {
                crouch = false;
            }
        }

        if (hitInfo.collider != null) {
            //Debug.DrawLine(transform.position, hitInfo.point, Color.red);
        } else {
            if (!Input.GetButton("Crouch")) {
                crouch = false;
            }
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate () {
        if (dead == false) {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }

        if (dead == true) {
            OnLanding();
            animator.SetBool("Dead", true);
            /*if (GetComponent<CharacterController2D>().enabled == true) {
                if (GetComponent<CharacterController2D>().m_Grounded == true) {
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    GetComponent<BoxCollider2D>().enabled = false;
                    GetComponent<CircleCollider2D>().enabled = false;
                    GetComponent<CharacterController2D>().enabled = false;
                }
            }*/
        }
    }

    void OnCollisionEnter2D (Collision2D col) {
        if (col.gameObject.tag == "Enemy") {
            dead = true;
            animator.SetBool("Dead", true);
            //GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            //rb.constraints = RigidbodyConstraints2D.FreezeAll;
            if (col.gameObject.name.Contains("Snowball")) {
                Destroy(col.gameObject);
            }
        }
    }
}
