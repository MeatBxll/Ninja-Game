using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diveBomber : MonoBehaviour
{


    public float agroRange;
    public float moveSpeed;
    public float dropSpeed;


    private GameObject player;
    private bool flight;
    private Rigidbody2D rb;


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
            if (!flight)
            {
                if (transform.position.x - agroRange < player.transform.position.x)
                {
                    if (transform.position.x + agroRange > player.transform.position.x)
                    {
                        flight = true;
                    }
                }

                rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);

            }
            else
            {
                rb.linearVelocity = new Vector2(0, -dropSpeed);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }


}
