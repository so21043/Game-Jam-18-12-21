using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 100f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    // Start is called before the first frame update
    void Start () {
        
    }

    // Update is called once per frame
    void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

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

        //Debug.DrawLine(transform.position, transform.position + transform.up * 3f, Color.green);
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
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        
    }
}