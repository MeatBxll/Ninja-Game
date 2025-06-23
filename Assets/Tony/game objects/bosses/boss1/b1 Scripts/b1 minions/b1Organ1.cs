using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b1Organ1 : MonoBehaviour
{
    private bool hitGround;
    private bool walking;

    public float organFireSpeed = 15;
    public float organWalkSpeed = 15;

    private Rigidbody2D rb;
    private GameObject player;
    private GameObject boss1;
    private Vector2 moveDirection;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        moveDirection = (player.transform.position - transform.position).normalized  * organFireSpeed;
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
