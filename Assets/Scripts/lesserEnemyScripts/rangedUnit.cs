using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangedUnit : MonoBehaviour
{
    public float maxAgroRange;
    public float minAgroRange;
    public float moveSpeed;
    public float fireRate;
    public float ammoSpeed;

    private float nextFire;
    private float shootDistance;

    public GameObject rangedUnitBullet;


    private GameObject currentBullet;
    private GameObject player;
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
            if (Time.time > nextFire)
            {
                currentBullet = Instantiate(rangedUnitBullet, new Vector2(transform.position.x + shootDistance, transform.position.y), transform.rotation);
                currentBullet.GetComponent<rangedUnitAmmo>().speed = ammoSpeed;
                nextFire = Time.time + fireRate;
            }

            float distToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distToPlayer < maxAgroRange & distToPlayer > minAgroRange)
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
            else
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }
            if (transform.position.x < player.transform.position.x)
            {
                shootDistance = 4;
            }
            else
            {
                shootDistance = -4;
            }

        }
    }
}
