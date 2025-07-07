using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss4S2 : MonoBehaviour
{
    //stage 2 - half pretty lady half spider
    //jumps across screen creating a web and if the player jumps into the web they will be stuck there for 3 seconds
    private bool jumps;
    private float letGoHolder;
    public GameObject spiderWebOne;
    public float JumpSpeed;
    public float JumpHeight;
    public float holdWebTime;


    //runs at the player
    private bool runs;
    private float playerPosition;
    public float runsAtSpeed;
    public float runPastDistance;
    public float runBackDurration;


    //shoots a normal web at the player
    private bool shoots;
    private float timeBetweenHolder;
    private float afterShotHolder;
    public GameObject spiderWebTwo;
    public float distanceFromBody;
    public float timeBetweenShootAndJump;
    public float afterShootSpeed;
    public float afterShootHeight;
    public float afterShotDurration;


    //lays an spider web egg then climbs off the screen and 3 smaller spiders hatch out and jump at the player doing different things then she climbs back onto the screen
    private bool lays;
    private float layRunHolder;
    private float jumpOffHolder;
    private float jumpOffDurrationHolder;
    public float layRunSpeed;
    public float layRunDurration;
    public float jumpOffHeight;
    public float holdOffGroundHeight;
    public GameObject egg;
    public float jumpOffDurration;
    public float downForceAfter;
    public float downDurrationAfter;


    //walk 
    private bool walk;
    private float walkDurrationHolder;
    public float walkDurration;
    public float moveSpeed;


    //general stuff
    private bool onlyOnce;
    private float holder;
    private float resetHolder;
    private Rigidbody2D rb;
    private GameObject player;



    void Start()
    {
        Debug.Log("stage 2 begun");
        rb = GetComponent<Rigidbody2D>();

        GetComponent<Health>().GainHealth(-1);


        ResetBoss();
        //jumps = true;
        //runs = true;
        //shoots = true;
        //lays = true;
    }

    void Update()
    {
        player = GameObject.FindWithTag("Player");
        if (GetComponent<Health>().currentHealth <= 0)
        {
            BossDies();
        }









        //reset
        if(resetHolder != 0)
        {
            if(resetHolder < Time.time)
            {
                holder = Random.Range(1, 5);
                resetHolder = 0;
            }
        }

        if(holder != 0)
        {
            if(holder == 1)
            {
                Debug.Log("jumps chosen");
                jumps = true;
                runs = false;
                shoots = false;
                lays = false;
                walk = false;
            }
            else if (holder == 2)
            {
                Debug.Log("runs chosen");
                jumps = false;
                runs = true;
                shoots = false;
                lays = false;
                walk = false;

            }
            else if (holder == 3)
            {
                Debug.Log("shoots chosen");
                jumps = false;
                runs = false;
                shoots = true;
                lays = false;
                walk = false;

            }
            else if (holder == 4)
            {
                Debug.Log("lays chosen");
                jumps = false;
                runs = false;
                shoots = false;
                lays = true;
                walk = false;

            }
            else
            {
                Debug.Log("wtf");
            }
            holder = 0;
        }






        //abilities
        if(jumps == true)
        {
            JumpsAt();
        }
        if (runs == true)
        {
            RunsAt();
        }
        if (shoots == true)
        {
            ShootsWeb();
        }
        if (lays == true)
        {
            LaysEgg();
        }
        if (walk == true)
        {
            WalksAround();
        }
    }




    //stage 2 - half pretty lady half spider
    //jumps across screen creating a web and if the player jumps into the web they will be stuck there for 3 seconds
    void JumpsAt()
    {
        if(onlyOnce == false)
        {
            if (transform.position.x < player.transform.position.x)
            {
                rb.velocity = new Vector2(JumpSpeed, JumpHeight);
            }
            else
            {
                rb.velocity = new Vector2(-JumpSpeed, JumpHeight);
            }
            
            Instantiate(spiderWebOne, new Vector2(transform.position.x, transform.position.y), transform.rotation);

            
            letGoHolder = holdWebTime + Time.time;
            onlyOnce = true;
        }

        if (letGoHolder != 0)
        {
            
            if (letGoHolder < Time.time)
            {
                transform.DetachChildren();
                letGoHolder = 0;
                ResetBoss();
            }
        }

    }



    //runs at the player
    void RunsAt()
    {
        if (onlyOnce == false)
        {
            if (transform.position.x < player.transform.position.x)
            {
                rb.velocity = new Vector2(runsAtSpeed, rb.velocity.y);
                playerPosition = 1;
            }
            else
            {
                rb.velocity = new Vector2(-runsAtSpeed, rb.velocity.y);
                playerPosition = 2;
            }
            onlyOnce = true;
        }
        //1 = the player is on the right so the boss runs right then needs to be sent back left
        //2 = the player is left of the boss
        if(playerPosition != 0)
        {
            if (playerPosition == 1)
            {
                if (transform.position.x > player.transform.position.x + runPastDistance)
                {
                    rb.velocity = new Vector2(-runsAtSpeed / 2, rb.velocity.y);
                    playerPosition = 0;
                    Invoke("ResetBoss", runBackDurration);
                }
            }
            else if(playerPosition == 2)
            {
                if (transform.position.x < player.transform.position.x - runPastDistance)
                {
                    rb.velocity = new Vector2(runsAtSpeed / 2, rb.velocity.y);
                    playerPosition = 0;
                    Invoke("ResetBoss", runBackDurration);
                }
            }
        }
    }



    //shoots a normal web at the player
    void ShootsWeb()
    {
        if(onlyOnce == false)
        {
            if (transform.position.x < player.transform.position.x)
            {
                Instantiate(spiderWebTwo, new Vector2(transform.position.x + distanceFromBody, transform.position.y), transform.rotation);
            }
            else
            {
                Instantiate(spiderWebTwo, new Vector2(transform.position.x - distanceFromBody, transform.position.y), transform.rotation);
            }
            timeBetweenHolder = Time.time + timeBetweenShootAndJump;
            onlyOnce = true;
        }

        if(timeBetweenHolder != 0)
        {
            if (timeBetweenHolder < Time.time)
            {
                if (transform.position.x < player.transform.position.x)
                {
                    rb.velocity = new Vector2(afterShootSpeed, afterShootHeight);
                    afterShotHolder = afterShotDurration + Time.time;
                    timeBetweenHolder = 0;
                }
                else
                {
                    rb.velocity = new Vector2(-afterShootSpeed, afterShootHeight);
                    afterShotHolder = afterShotDurration + Time.time;
                    timeBetweenHolder = 0;

                }
                
            }
        }

        if(afterShotHolder != 0)
        {
            if(afterShotHolder < Time.time)
            {
                rb.velocity = new Vector2(0, 0);
                afterShotHolder = 0;
                ResetBoss();
            }
        }



    }





    //lays an spider web egg then climbs off the screen and 3 smaller spiders hatch out and jump at the player doing different things then she climbs back onto the screen
    void LaysEgg()
    {
        if(onlyOnce == false)
        {
            if (transform.position.x < player.transform.position.x)
            {
                rb.velocity = new Vector2(layRunSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-layRunSpeed, rb.velocity.y);
            }

            layRunHolder = layRunDurration + Time.time;
            onlyOnce = true;
        }

        if(layRunHolder != 0)
        {
            if(layRunHolder < Time.time)
            {
                rb.velocity = new Vector2(0, jumpOffHeight);
                Instantiate(egg, transform.position, transform.rotation);
                layRunHolder = 0;

                jumpOffHolder = Time.time + 2;
                jumpOffDurrationHolder = Time.time + jumpOffDurration;
            }
        }

        if(jumpOffHolder != 0)
        {
            if (jumpOffHolder < Time.time)
            {
                transform.position = new Vector2(player.transform.position.x, player.transform.position.y + holdOffGroundHeight);
            }
        }
        
        if(jumpOffDurrationHolder != 0)
        {
            if (jumpOffDurrationHolder < Time.time)
            {
                jumpOffHolder = 0;
                rb.velocity = new Vector2(0, -downForceAfter);
                jumpOffDurrationHolder = 0;
                Invoke("ResetBoss", downDurrationAfter);
            }
        }

    }




    // between abilities the boss needs to do something special
    void WalksAround()
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









    //reset boss
    void ResetBoss()
    {
        Debug.Log("boss reset");
        onlyOnce = false;
        walk = true;
        resetHolder = walkDurration + Time.time;


        jumps = false;
        runs = false;
        shoots = false;
        lays = false;
        CancelInvoke();
    }





    void BossDies()
    {
        //play animation - the trasition from the last stage to this one is where she cacoons up and a bunch of tiny spiders crawl out while she is then she hatches out and all the baby spiders run offscreen


        //next stage
        GetComponent<boss4S3>().enabled = true;
        GetComponent<boss4S2>().enabled = false;

    }
}