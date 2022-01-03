using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpForce;

    [SerializeField] Rigidbody2D rb;
    groundCheck groundCheck;
    bool grounded;

    void Start()
    {
        groundCheck = transform.Find("GroundCheck").gameObject.GetComponent(typeof(groundCheck)) as groundCheck;
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f,jumpForce));
    }
    
    void Update()
    {
        grounded = groundCheck.grounded;
        if (Input.touchCount > 0)
        {
             Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && grounded)
            {
             Jump();
            }
        }
    }
}
