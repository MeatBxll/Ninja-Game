using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingShotGunShooter : MonoBehaviour
{
    public float fireRate;
    private float nextFire;

    public float agroRange;
    public float moveSpeed;

    private GameObject player;
    private Rigidbody2D rb;


    //closestPlayer Variables 
    private GameObject[] players;
    private float close;

    //shotgun variables
    public GameObject shell;
    public float shellSpeed = 20f;
    public GameObject[] shellAmount;
    public float spreadAngle;

    private float currentSpread;



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
                for (int i = 0; i < shellAmount.Length; i++)
                {
                    shellAmount[i] = Instantiate(shell, new Vector2(transform.position.x,transform.position.y - 1), Quaternion.Euler(0, 0, (-90 - (spreadAngle * (shellAmount.Length * 0.5f - 0.5f))) + currentSpread));
                    currentSpread = spreadAngle * (i + 1);
                    if(i == shellAmount.Length - 1)
                    {
                        currentSpread = 0;
                    }
                }

                foreach (GameObject sGAmmo in shellAmount)
                {
                    sGAmmo.GetComponent<enemyBullet>().speed = shellSpeed;
                }




                nextFire = Time.time + fireRate;
            }
        }
    }
}
