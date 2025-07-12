using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b1Organ4 : MonoBehaviour
{
    private bool hitGround;
    private bool walking;

    private float nextJump;
    private float dashHolder;
    private float resetHolder;

    public float jumpHeight;
    public float timeBetweenJumps;
    public float organFireSpeed = 15;
    public float organWalkSpeed = 15;
    public float dashTimeAfterJump;
    public float dashForce;
    public float dashDurration;

    private Rigidbody2D rb;
    private GameObject player;
    private GameObject boss1;
    private Vector2 moveDirection;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        moveDirection = (player.transform.position - transform.position).normalized * organFireSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    void Update()
    {
        boss1 = GameObject.FindWithTag("boss1");

        if (hitGround == true)
        {
            if (walking == false)
            {
                if (transform.position.x < boss1.transform.position.x)
                {
                    rb.velocity = new Vector2(organWalkSpeed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(-organWalkSpeed, rb.velocity.y);
                }
            }

            if (Time.time > nextJump)
            {
                rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                nextJump = Time.time + timeBetweenJumps;
                dashHolder = Time.time + dashTimeAfterJump;

            }

            if(dashHolder != 0)
            {
                if(dashHolder < Time.time)
                {
                    walking = true;
                    if (transform.position.x < boss1.transform.position.x)
                    {
                        rb.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
                    }
                    else
                    {
                        rb.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
                    }
                    dashHolder = 0;
                    resetHolder = Time.time + dashDurration;
                }
            }

            if(resetHolder != 0)
            {
                if(resetHolder < Time.time)
                {
                    walking = false;
                    resetHolder = 0;

                }
            }



            if (transform.position.x > boss1.transform.position.x - 1)
            {
                if (transform.position.x < boss1.transform.position.x + 1)
                {
                    moveDirection = (boss1.transform.position - transform.position).normalized * organFireSpeed;
                    walking = true;
                    rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
                }
            }

            if (transform.position.x > boss1.transform.position.y - 2)
            {
                if (transform.position.x < boss1.transform.position.y + 2)
                {
                    Destroy(gameObject, 0);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            hitGround = true;
            rb.velocity = new Vector2(0, 0);
        }

        if (collision.gameObject.tag == "b1BloodPuddle")
        {
            hitGround = true;
            rb.velocity = new Vector2(0, 0);
        }

        if (collision.gameObject.tag == "boss1")
        {
            Destroy(gameObject, 0);
        }
    }
}
