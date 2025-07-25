using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpingZombie : MonoBehaviour
{
    private GameObject player;
    public float agroRange;
    public float moveSpeed;
    private float nextJump;
    public float timeBetweenJumps;
    public float jumpHeight;
    Rigidbody2D rb;

    //closestPlayer Variables 
    private GameObject[] players;
    private float close;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextJump = Time.time;

    }


    void Update()
    {
        //checking closest player
        players = GameObject.FindGameObjectsWithTag("Player");


        close = Mathf.Infinity;
        foreach (GameObject pl in players)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, pl.transform.position);
            if (distanceToPlayer < close)
            {
                close = distanceToPlayer;
                player = pl;
            }
        }

        if (player == null)
        {
            Destroy(gameObject);
        }

        //put everything within void update into this else statement
        else
        {

            if (Time.time > nextJump)
            {
                rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                nextJump = Time.time + timeBetweenJumps;

            }


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
    }





    void ChasePlayer()
    {
        if (transform.position.x < player.transform.position.x)
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
        }
    }

    void StopChasingPlayer()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }


}
