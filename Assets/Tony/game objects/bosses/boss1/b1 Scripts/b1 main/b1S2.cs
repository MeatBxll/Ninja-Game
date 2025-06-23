using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b1S2 : MonoBehaviour
{
    //slam fists down
    private bool fistsSlam;
    public float fistsSlamResetTime;


    //send bats at player
    private bool sendBats;
    public float sendBatsResetTime;
    public GameObject batCluster;


    //head slam (head flies out of the body with the spine intact and slams head on the player)
    private bool headSlam;
    public float headSlamResetTime;


    // lick 
    private bool lick;
    public float lickResetTime;

    //universal things 
    private bool resetBoss;
    private bool onlyOnce;
    private float resetHolder;
    private float randomHolder;
    private float randomRangeHolder;
    public Transform normalBarrelEnd;


    //walk
    private bool walk;
    public float walkTime;
    public float walkSpeed;


    //important game objects 
    private GameObject player;
    private Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Health>().GainHealth(-1);
        Debug.Log("stage two has begun");

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
                Debug.Log("fists slam chosen");

                fistsSlam = true;
                sendBats = false;
                headSlam = false;
                lick = false;

                walk = false;
            }
            else if (randomRangeHolder == 2)
            {
                Debug.Log("send bats chosen");

                fistsSlam = false;
                sendBats = true;
                headSlam = false;
                lick = false;

                walk = false;
            }
            else if (randomRangeHolder == 3)
            {
                Debug.Log("head slam chosen");

                fistsSlam = false;
                sendBats = false;
                headSlam = true;
                lick = false;

                walk = false;
            }
            else if (randomRangeHolder == 4)
            {
                Debug.Log("lick chosen");

                fistsSlam = false;
                sendBats = false;
                headSlam = false;
                lick = true;

                walk = false;
            }
            else
            {
                Debug.Log("wtf");
            }

            randomRangeHolder = 0;
        }







        //slam fists down
        if (fistsSlam == true)
        {
            if (onlyOnce == false)
            {
                resetHolder = fistsSlamResetTime + Time.time;
                onlyOnce = true;
            }
        }

        //send bats at player
        if (sendBats == true)
        {
            if (onlyOnce == false)
            {
                rb.velocity = new Vector2(0, 0);
                Instantiate(batCluster, normalBarrelEnd.transform.position, normalBarrelEnd.rotation);

                resetHolder = sendBatsResetTime + Time.time;
                onlyOnce = true;
            }

        }


        //head slam (head flies out of the body with the spine intact and slams head on the player)
        if (headSlam == true)
        {
            if (onlyOnce == false)
            {

                resetHolder = headSlamResetTime + Time.time;
                onlyOnce = true;
            }

        }


        // lick 
        if (lick == true)
        {
            if (onlyOnce == false)
            {
                resetHolder = lickResetTime + Time.time;
                onlyOnce = true;
            }

        }





        //normal Walking State
        if (walk == true)
        {
            if (transform.position.x < player.transform.position.x)
            {
                rb.velocity = new Vector2(walkSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-walkSpeed, rb.velocity.y);
            }
        }




        //Reset stage two
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

            walk = true;

            fistsSlam = false;
            sendBats = false;
            headSlam = false;
            lick = false;


            randomHolder = walkTime + Time.time;
            onlyOnce = false;
            resetBoss = true;
        }






        if (GetComponent<Health>().currentHealth <= 0)
        {
            //play next stage animation



            //turn off all the stage two abilities
            walk = false;
            fistsSlam = false;
            sendBats = false;
            headSlam = false;
            lick = false;


            //turn off all the stage two stage setting prompts
            resetHolder = 0;
            randomHolder = 0;
            resetBoss = true;
            randomRangeHolder = 0;


            //reset velocity
            rb.velocity = new Vector2(0, 0);


            //set stage
            GetComponent<b1S3>().enabled = true;
            GetComponent<b1S2>().enabled = false;
        }
    }
}
