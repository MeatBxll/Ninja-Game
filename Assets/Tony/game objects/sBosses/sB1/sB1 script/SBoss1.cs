using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBoss1 : MonoBehaviour
{
    
    private float nextJump;
    private float stageTwo = 1;
    private bool healthOnlyOnce;


    public float agroRange;
    public float moveSpeed;
    public float timeBetweenJumps;
    public float slam;
    public float jumpHeight;
    public float stageTwoMultiplier;


    GameObject player;
    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextJump = Time.time;
    }


    void Update()
    {
        //when the boss has no more health
        if (GetComponent<Health>().currentHealth <= 0)
        {
            BossDies();
        }

        //when boss gets half health
        if(GetComponent<Health>().halfHealth >= GetComponent<Health>().currentHealth)
        {
            if(healthOnlyOnce == false)
            {
                //play stage two animation


                Debug.Log("boss half health stage two started");
                stageTwo = stageTwoMultiplier;
                healthOnlyOnce = true;
            }
        }

        //slam thing
        if (Time.time > nextJump)
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            Invoke("slamDown", 1);
            nextJump = Time.time + timeBetweenJumps;
        }



        player = GameObject.FindWithTag("Player");
        ChasePlayer();
    }









    void ChasePlayer()
    {
        if (transform.position.x < player.transform.position.x)
        {
            rb.velocity = new Vector2(moveSpeed * stageTwo, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed * stageTwo, rb.velocity.y);
        }
    }




    void BossDies()
    {
        //play die animation

        //destroy
        Destroy(gameObject,0);

    }




    void slamDown()
    {
        rb.AddForce(Vector2.down*slam*stageTwo);
        CancelInvoke();
    }
}
