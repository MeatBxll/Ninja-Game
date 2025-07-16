using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBoss3 : MonoBehaviour
{





    private bool noCast;
    private bool slam;
    private bool meteor;
    private bool teleport;
   

    public float moveSpeed;

    //Slam variables
    private bool onlyOnce;
    private float slamTimeHolder;
    private float leftOrRightSlam;


    public float slamSlideSpeed;
    public float slamSpeed;


    //meteor variables
    private float meteorRandom;

    public GameObject meteorGameObject;


    //teleport variables 
    private float teleportCurrentAmount;
    private float randomSpot;
    private float teleportTimerHolder;
    private bool teleportCheckPoint;

    public float timeBetweenTeleports;
    public float teleportAmount;
    public GameObject teleportBullets;
    public Transform leftBarrel;
    public Transform midBarrel;
    public Transform rightBarrel;



    //general variables

    private float resetHolder;
    private float eventHolder;

    public float resetTime;
    public float resetDelay;

    //the locations used by the boss to tell where to teleport to
    public float midheight;
    public float horizontalDistance;
    public float topMidHeight;


    private GameObject player;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //BossReset();
        meteor = true;
    }

    void Update()
    {
        player = GameObject.FindWithTag("Player");


        if (resetHolder != 0)
        {
            if (resetHolder < Time.time)
            {
                eventHolder = Random.Range(1, 4);
                if(eventHolder == 1)
                {
                    Debug.Log("slamSelected");
                    teleport = false;
                    meteor = false;
                    slam = true;
                    noCast = false;
                }
                else if (eventHolder == 2)
                {
                    Debug.Log("meteorSelected");
                    teleport = false;
                    meteor = true;
                    slam = false;
                    noCast = false;
                }
                else
                {
                    Debug.Log("teleportSelected");
                    teleport = true;
                    meteor = false;
                    slam = false;
                    noCast = false;
                }

                resetHolder = 0;
            }
        }
        



        










        if (noCast == true)
        {
            
            ChasePlayer();
        }
        if (slam == true)
        {
            SlamDown();
        }
        if (meteor == true)
        {
            
            MeteorStrike();
        }
        if (teleport == true)
        {
            
            TeleportBoss();
        }

        if (GetComponent<Health>().currentHealth <= 0)
        {
            BossDies();
        }

    }





    void ChasePlayer()
    {
        if (transform.position.x < player.transform.position.x)
        {
            rb.linearVelocity = new Vector2(moveSpeed , rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
        }
    }

    void SlamDown()
    {
        //one ability where he slams down and goes randomly left or right
        if (onlyOnce == false)
        {
            rb.linearVelocity = new Vector2(0, 0);
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y + topMidHeight);
            rb.AddForce(Vector2.up * 100);
            slamTimeHolder = Time.time +1;
            
            onlyOnce = true;

        }
        if (slamTimeHolder != 0)    
        {
            if (Time.time > slamTimeHolder)
            {

                rb.AddForce(Vector2.down * slamSpeed);
                slamTimeHolder = 0;
                leftOrRightSlam = Random.Range(1, 3);
            }
            
        }
    }

    void MeteorStrike()
    {
        //one ability where he hurls a large projectile that explodes and ripple effect on the ground
        if (onlyOnce == false)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            meteorRandom = Random.Range(1, 3);
            onlyOnce = true;

        }


        if (meteorRandom != 0)
        {
            if (meteorRandom == 1)
            {

                transform.position = new Vector2(player.transform.position.x - horizontalDistance, player.transform.position.y + midheight);
                meteorRandom = 0;
                Instantiate(meteorGameObject, new Vector2 (player.transform.position.x + horizontalDistance, player.transform.position.y + midheight), transform.rotation);
            }
            else
            {
                transform.position = new Vector2(player.transform.position.x + horizontalDistance, player.transform.position.y + midheight);
                meteorRandom = 0;
                Instantiate(meteorGameObject, new Vector2(player.transform.position.x - horizontalDistance, player.transform.position.y + midheight), transform.rotation);
            }

        }
        

        Invoke("BossReset", resetTime);
    }




    void TeleportBoss()
    {
        //one ability where he teleports to two random sides and shoots towards the middle
        if(teleportCurrentAmount != teleportAmount)
        {
            if(onlyOnce == false)
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
                randomSpot = Random.Range(1, 5);
                teleportCurrentAmount = teleportCurrentAmount + 1;
                onlyOnce = true;
            }
            
            

        }


        if(teleportCurrentAmount == teleportAmount)
        {
            Invoke("BossReset", resetTime);
        }

        if(randomSpot != 0)
        {
            if (randomSpot == 1)
            {
                transform.position = new Vector2(player.transform.position.x - horizontalDistance, player.transform.position.y + midheight);
                Invoke("ShotLeft", timeBetweenTeleports / 3);
                teleportTimerHolder = Time.time + timeBetweenTeleports;
                teleportCheckPoint = true;
                randomSpot = 0;
            }
            else if (randomSpot == 2)
            {
                transform.position = new Vector2(player.transform.position.x + horizontalDistance, player.transform.position.y + midheight);
                Invoke("ShotRight", timeBetweenTeleports / 3);
                teleportTimerHolder = Time.time + timeBetweenTeleports;
                teleportCheckPoint = true;
                randomSpot = 0;
            }
            else if (randomSpot == 3)
            {
                transform.position = new Vector2(player.transform.position.x, player.transform.position.y + midheight);
                Invoke("ShotMid", timeBetweenTeleports / 3);
                teleportTimerHolder = Time.time + timeBetweenTeleports;
                teleportCheckPoint = true;
                randomSpot = 0;
            }
            else
            {
                transform.position = new Vector2(player.transform.position.x, player.transform.position.y + topMidHeight);
                Invoke("ShotMid", timeBetweenTeleports / 3);
                teleportTimerHolder = Time.time + timeBetweenTeleports;
                teleportCheckPoint = true;
                randomSpot = 0;
            }
        }
        
        if(teleportCheckPoint == true)
        {
            if (teleportTimerHolder < Time.time)
            {
                onlyOnce = false;
                teleportCheckPoint = false;
            }
        }
        
        
    }
    void ShotLeft()
    {
        Instantiate(teleportBullets, leftBarrel.position, leftBarrel.rotation);
        CancelInvoke();
    }

    void ShotRight()
    {
        Instantiate(teleportBullets, rightBarrel.position, rightBarrel.rotation);
        CancelInvoke();
    }

    void ShotMid()
    {
        Instantiate(teleportBullets, midBarrel.position, midBarrel.rotation);
        CancelInvoke();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        //for slam


        if (slam == true)
        {
            if (collision.gameObject.tag == "ground")
            {
                if (leftOrRightSlam == 1)
                {
                    rb.AddForce(Vector2.right * slamSlideSpeed);
                    leftOrRightSlam = 0;
                    Invoke("BossReset", resetTime);
                }
                else
                {
                    rb.AddForce(Vector2.left * slamSlideSpeed);
                    leftOrRightSlam = 0;
                    Invoke("BossReset", resetTime);
                }

                
            }

        }

    }



    void BossReset()
    {
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y + midheight);
        slam = false;
        meteor = false;
        teleport = false;
        noCast = true;

        onlyOnce = false;
        teleportCurrentAmount = 0;

        resetHolder = Time.time + resetDelay;

        CancelInvoke();

    }

    void BossDies()
    {
        //play die animation

        //destroy
        Destroy(gameObject, 0);
    }
}


