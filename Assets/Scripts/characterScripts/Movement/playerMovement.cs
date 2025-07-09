using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public float movementSpeed;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDistance;

    private bool canMove = true;
    private bool canDash = true;
    private Rigidbody2D rb;
    private bool canJump;
    private bool isGrounded;
    private CapsuleCollider2D myCollider;

    private KeyCode jumpButton = KeyCode.Space;
    private KeyCode dashButton = KeyCode.LeftShift;

    void Start()
    {
        myCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canMove)
        {
            HandleJump();
            HandleMovement();
        }

        if (Input.GetKeyDown(dashButton) && canDash)
        {
            StartCoroutine(HandleDash());
            Debug.Log("dash pressed");
        }
    }
    void HandleJump()
    {
        if (Input.GetKeyDown(jumpButton))
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
            if (hit != myCollider)
                isGrounded = true;
            else
                isGrounded = false;
        }
        if (isGrounded)
            canJump = true;
    }

    void HandleMovement()
    {
        float inputX = Input.GetAxis("Horizontal");
        float desiredSpeed = inputX * movementSpeed;
        float currentX = rb.velocity.x;

        if (Mathf.Approximately(inputX, 0f))
            return;

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

    private IEnumerator HandleDash()
    {
        float gravityScale = rb.gravityScale;
        canDash = false;
        int i = 0;
        while (i < 4)
        {
            if (i == 0)
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = transform.position.z;
                Vector3 dashDirection = (mouseWorldPos - transform.position).normalized;

                rb.gravityScale = 0;
                rb.velocity = dashDirection * dashSpeed;
                canMove = false;
            }
            else if (i == 1)
            {
                canMove = true;
                rb.velocity = new Vector3(rb.velocity.x / 5, rb.velocity.y / 5, 0);
                rb.gravityScale = gravityScale;
            }

            i++;
            yield return new WaitForSeconds(dashDistance / dashSpeed);
        }

        canDash = true;
    }
}




