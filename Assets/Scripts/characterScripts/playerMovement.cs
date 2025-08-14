using System;
using System.Collections;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float movementSpeed;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDistance;

    [NonSerialized] public bool canMove = true;
    [NonSerialized] public bool isDashing = false;

    private Rigidbody2D rb;
    private bool canJump = true;
    private bool isGrounded;
    private CapsuleCollider2D myCollider;

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
            if (UserInput.instance.dashInput && !isDashing)
                StartCoroutine(HandleDash());
        }
    }

    void HandleJump()
    {
        if (UserInput.instance.jumpInput)
        {
            if (canJump)
            {
                rb.linearVelocity = Vector2.up * jumpMultiplier;
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
        float currentX = rb.linearVelocity.x;

        if (Mathf.Approximately(inputX, 0f))
            return;

        if (Mathf.Sign(desiredSpeed) == Mathf.Sign(currentX))
        {
            if (Mathf.Abs(currentX) < Mathf.Abs(desiredSpeed))
            {
                rb.linearVelocity = new Vector2(desiredSpeed, rb.linearVelocity.y);
            }
        }
        else
        {
            float resistanceForce = desiredSpeed * 0.5f;
            rb.linearVelocity = new Vector2(currentX + resistanceForce, rb.linearVelocity.y);
        }
    }

    private IEnumerator HandleDash()
    {
        float gravityScale = rb.gravityScale;
        isDashing = true;
        int i = 0;
        while (i < 7)
        {
            if (i == 0)
            {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = transform.position.z;
                Vector3 dashDirection = (mouseWorldPos - transform.position).normalized;

                rb.gravityScale = 0;
                rb.linearVelocity = dashDirection * dashSpeed;
                canMove = false;
            }
            else if (i == 1)
            {
                canMove = true;
                rb.linearVelocity = new Vector3(rb.linearVelocity.x / 5, rb.linearVelocity.y / 5, 0);
                rb.gravityScale = gravityScale;
            }

            i++;
            yield return new WaitForSeconds(dashDistance / dashSpeed);
        }

        isDashing = false;
    }
}




