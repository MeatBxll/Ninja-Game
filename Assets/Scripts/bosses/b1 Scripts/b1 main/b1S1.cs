using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b1S1 : MonoBehaviour
{
    //slam sword
    private bool slamSword;
    private Vector2 moveDirection;
    private float slamSwordHolder;
    public float slamSwordResetTime;
    public float slamSwordJumpHeight;
    public float slamSwordJumpDurration;
    public float slamSwordSlamSpeed;
    public float swordSize;


    //cape slash
    private bool capeSlash;
    public float capeSlashResetTime;

    //quick slash
    private bool quickSlash;
    public float quickSlashResetTime;

    //player grab
    private bool playerGrab;
    public float playerGrabResetTime;
    public float grabForce;

    //walk
    private bool stageOneWalk;
    public float stageOneWalkTime;
    public float walkSpeed;





    //universal things 
    private bool resetBoss;
    private bool onlyOnce;
    private float resetHolder;
    private float randomHolder;
    private float randomRangeHolder;
    private Rigidbody2D playerRb;


    //important game objects 
    private GameObject player;
    private Rigidbody2D rb;








    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        player = GameObject.FindWithTag("Player");


        //ability picker
        if (randomHolder != 0)
        {
            if (randomHolder < Time.time)
            {
                randomRangeHolder = Random.Range(1, 5);
                randomHolder = 0;
            }
        }
        if (randomRangeHolder != 0)
        {
            if (randomRangeHolder == 1)
            {
                Debug.Log("slam sword chosen");

                slamSword = true;
                capeSlash = false;
                quickSlash = false;
                playerGrab = false;

                stageOneWalk = false;
            }
            else if (randomRangeHolder == 2)
            {
                Debug.Log("cape slash chosen");

                slamSword = false;
                capeSlash = true;
                quickSlash = false;
                playerGrab = false;

                stageOneWalk = false;
            }
            else if (randomRangeHolder == 3)
            {
                Debug.Log("quick slash chosen");

                slamSword = false;
                capeSlash = false;
                quickSlash = true;
                playerGrab = false;

                stageOneWalk = false;
            }
            else if (randomRangeHolder == 4)
            {
                Debug.Log("player grab chosen");

                slamSword = false;
                capeSlash = false;
                quickSlash = false;
                playerGrab = true;

                stageOneWalk = false;
            }
            else
            {
                Debug.Log("wtf");
            }

            randomRangeHolder = 0;
        }




        //slam sword
        if (slamSword == true)
        {
            if (onlyOnce == false)
            {
                onlyOnce = true;

                rb.AddForce(Vector2.up * slamSwordJumpHeight, ForceMode2D.Impulse);

                slamSwordHolder = Time.time + slamSwordJumpDurration;

            }
        }
        if (slamSwordHolder != 0)
        {
            if (Time.time > slamSwordHolder)
            {
                moveDirection = (player.transform.position - transform.position).normalized * slamSwordSlamSpeed;
                if (transform.position.x < player.transform.position.x)
                {
                    rb.linearVelocity = new Vector2(moveDirection.x - swordSize, moveDirection.y);
                }
                else
                {
                    rb.linearVelocity = new Vector2(moveDirection.x + swordSize, moveDirection.y);
                }



                slamSwordHolder = 0;
                resetHolder = playerGrabResetTime + Time.time;
            }
        }


        //cape slash
        if (capeSlash == true)
        {
            if (onlyOnce == false)
            {
                onlyOnce = true;
                resetHolder = playerGrabResetTime + Time.time;
            }
        }



        //quick Slash
        if (quickSlash == true)
        {
            if (onlyOnce == false)
            {
                onlyOnce = true;
                resetHolder = playerGrabResetTime + Time.time;
            }
        }


        //player grab
        if (playerGrab == true)
        {
            if (onlyOnce == false)
            {
                onlyOnce = true;
                resetHolder = playerGrabResetTime + Time.time;
                rb.linearVelocity = new Vector2(0, 0);

                playerRb = player.GetComponent<Rigidbody2D>();

                if (transform.position.x < player.transform.position.x)
                {
                    playerRb.linearVelocity = new Vector2(-grabForce, playerRb.linearVelocity.y);
                }
                else
                {
                    playerRb.linearVelocity = new Vector2(grabForce, playerRb.linearVelocity.y);
                }
            }

            //slice up with the sword like an upercut kinda sorta typa thingie


        }



        //normal Walking State
        if (stageOneWalk == true)
        {
            if (transform.position.x < player.transform.position.x)
            {
                rb.linearVelocity = new Vector2(walkSpeed, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(-walkSpeed, rb.linearVelocity.y);
            }
        }




        //Reset stage one
        if (resetHolder != 0)
        {
            if (resetHolder < Time.time)
            {
                Debug.Log("boss reset");
                resetBoss = false;
                resetHolder = 0;
            }
        }


        if (resetBoss == false)
        {

            stageOneWalk = true;

            slamSword = false;
            capeSlash = false;
            quickSlash = false;
            playerGrab = false;


            randomHolder = stageOneWalkTime + Time.time;
            onlyOnce = false;
            resetBoss = true;
        }




        if (GetComponent<Health>().currentHealth <= 0)
        {
            //play next stage animation



            //start stage two
            slamSword = false;
            capeSlash = false;
            quickSlash = false;
            playerGrab = false;
            stageOneWalk = false;

            randomHolder = 0;
            randomRangeHolder = 0;

            resetBoss = false;


            //set stage
            GetComponent<b1S2>().enabled = true;
            GetComponent<b1S1>().enabled = false;
        }
    }
}
