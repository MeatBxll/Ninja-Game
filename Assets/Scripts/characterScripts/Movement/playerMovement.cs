using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public float movementSpeed;
    [SerializeField] private float jumpMultiplier;


    private Rigidbody2D rb;
    private bool canJump;
    private bool isGrounded;
    private CapsuleCollider2D myCollider;


    void Start()
    {
        myCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleJump();
        HandleMovement();
    }
    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump)
            {
                rb.velocity = Vector2.up * jumpMultiplier;
                canJump = false;
            }
        }

        Vector3 GroundCheckPosition = new Vector3(transform.position.x, transform.position.y - myCollider.size.y / 2, 0);
        Collider2D[] hits = Physics2D.OverlapCircleAll(GroundCheckPosition, 0.2f);

        foreach (Collider2D hit in hits)
        {
            if (hit != myCollider) isGrounded = true;
            else isGrounded = false;
        }
        if (isGrounded) canJump = true;
    }

    void HandleMovement()
{
    float inputX = Input.GetAxis("Horizontal");
    float desiredSpeed = inputX * movementSpeed;
    float currentX = rb.velocity.x;

    if (Mathf.Approximately(inputX, 0f)) return;

    if (Mathf.Sign(desiredSpeed) == Mathf.Sign(currentX))
    {
        if (Mathf.Abs(currentX) < Mathf.Abs(desiredSpeed))
        {
            rb.velocity = new Vector2(desiredSpeed, rb.velocity.y);
        }
    }
    else
    {
        float resistanceForce = desiredSpeed * 0.5f;
        rb.velocity = new Vector2(currentX + resistanceForce, rb.velocity.y);
    }
}


}




