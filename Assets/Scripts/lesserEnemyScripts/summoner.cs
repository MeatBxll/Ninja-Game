using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class summoner : MonoBehaviour
{

    public float maxAgroRange;
    public float minAgroRange;
    public float moveSpeed;
    public float fireRate;
    public float ammoSpeed;
    public float fallSpeed;
    public float tossAngle;


    private float nextFire;
    private float shootDistance;
    private float currentAngle;

    public GameObject summonerThing;


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
                currentBullet = Instantiate(summonerThing, new Vector2(transform.position.x + shootDistance, transform.position.y), Quaternion.Euler(0, currentAngle, tossAngle));
                currentBullet.GetComponent<summonerThing>().speed = ammoSpeed;
                currentBullet.GetComponent<Rigidbody2D>().gravityScale = fallSpeed;
                nextFire = Time.time + fireRate;
            }

            float distToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (distToPlayer < maxAgroRange & distToPlayer > minAgroRange)
            {
                if (transform.position.x < player.transform.position.x)
                {
                    rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
                    shootDistance = 4;
                    currentAngle = 0;
                }
                else
                {
                    rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
                    shootDistance = -4;
                    currentAngle = 180;
                }
            }
            else
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }
        }
    }
}
