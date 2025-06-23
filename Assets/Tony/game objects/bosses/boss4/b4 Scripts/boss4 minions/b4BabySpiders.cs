using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b4BabySpiders : MonoBehaviour
{
    private GameObject player;
    public float agroRange;
    public float moveSpeed;
    private float nextJump;
    public float timeBetweenJumps;
    public float jumpHeight;
    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextJump = Time.time;

    }


    void Update()
    {
        if (Time.time > nextJump)
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            nextJump = Time.time + timeBetweenJumps;

        }



        player = GameObject.FindWithTag("Player");

        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distToPlayer < agroRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasingPlayer();
        }
    }
    void ChasePlayer()
    {
        if (transform.position.x < player.transform.position.x)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
    }

    void StopChasingPlayer()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }


}