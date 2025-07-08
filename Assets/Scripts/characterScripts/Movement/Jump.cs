using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{


    public float jumpMultiplier;


    private Rigidbody2D rigidbodyComponent;
    private int availableJumps;
    private bool isGrounded;
    private CapsuleCollider2D myCollider;


    void Start()
    {
        myCollider = GetComponent<CapsuleCollider2D>();
        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (availableJumps != 0)
            {

                rigidbodyComponent.velocity = Vector2.up * jumpMultiplier;
                availableJumps--;
                Invoke("ResetJump",  0.2f);
            }
        }

        Vector3 GroundCheckPosition = new Vector3(transform.position.x, transform.position.y - myCollider.size.y / 2, 0);
        Collider2D[] hits = Physics2D.OverlapCircleAll(GroundCheckPosition, 0.2f);
        if (hits.Length == 0) isGrounded = false;
        
        foreach (Collider2D hit in hits)
        {
            if (hit != myCollider)
            {
                isGrounded = true;
                break;
            }
        }
        Debug.Log(isGrounded);
        
        if (isGrounded) availableJumps = 1;

    }

    private void ResetJump()
    {
        availableJumps = 1;
    }
}



