using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingShooter : MonoBehaviour
{
    //Bullet variables
    public GameObject bullet;
    public float bulletSpeed;

    private GameObject currentBullet;

    public float fireRate;
    private float nextFire;

    private GameObject player;
    public float agroRange;
    public float moveSpeed;

    private Rigidbody2D rb;

    //closestPlayer Variables 
    private GameObject[] players;
    private float close;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextFire = Time.time;
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
            }
            else
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }


            if (Time.time > nextFire)
            {
                currentBullet = Instantiate(bullet, new Vector2(transform.position.x, transform.position.y - 1), Quaternion.Euler(0, 0, -90));
                currentBullet.GetComponent<enemyBullet>().speed = bulletSpeed;

                nextFire = Time.time + fireRate;
            }
        }

    }
}
