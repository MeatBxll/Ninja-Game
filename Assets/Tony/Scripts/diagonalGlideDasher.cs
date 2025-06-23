using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diagonalGlideDasher : MonoBehaviour
{

    private GameObject player;
    private bool onlyOnce;

    public float agroRange;
    public float moveSpeed;

    Rigidbody2D rb;
    public float diveSpeed;
    public float roamSpeed;
    public float gravityScale;


    //closestPlayer Variables 
    private GameObject[] players;
    private float close;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
            if (transform.position.x - agroRange < player.transform.position.x)
            {
                if (transform.position.x + agroRange > player.transform.position.x)
                {
                    if (transform.position.x < player.transform.position.x)
                    {
                        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                    }

                    if (!onlyOnce)
                    {
                        rb.gravityScale = gravityScale;
                        rb.velocity = new Vector2(rb.velocity.x, -diveSpeed);
                        onlyOnce = true;
                    }
                }
                else
                {
                    rb.velocity = new Vector2(roamSpeed, rb.velocity.y);
                }
            }
            else
            {
                rb.velocity = new Vector2(roamSpeed, rb.velocity.y);
            }
        }
    }
}
