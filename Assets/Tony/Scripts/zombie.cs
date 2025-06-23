using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : MonoBehaviour
{

    private GameObject player;
    public float agroRange;
    public float moveSpeed;

    //closestPlayer Variables 
    private GameObject[] players;
    private float close;

    Rigidbody2D rb;


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
            //normal zombie stuff
            float distToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distToPlayer < agroRange)
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
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

    }

}
