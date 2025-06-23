using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b2S2 : MonoBehaviour
{
    //flails at the player 
    private bool abilityOne;

    //stomps on the ground causing a big rock to come out and throws it at the player 
    private bool abilityTwo;

    //runs at the player and slams on the ground when he gets close causing a big shockwave 
    private bool abilityThree;
    private bool runsAtPlayerOnlyOnce;
    private bool runsAtPlayerOnlyOnceTwo;
    public float runsAtPlayerSpeed;

    //rips something out of the background and throws it at the player
    private bool abilityFour;



    //important variables
    private bool onlyOnce;
    private bool resetBoss;
    public float resetTime;
    private float resetHolder;
    private float randomHolder;
    private float randomRangeHolder;



    //walk variables
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
                Debug.Log("shoots organs chosen");

                abilityOne = true;
                abilityTwo = false;
                abilityThree = false;
                abilityFour = false;

                walk = false;
            }
            else if (randomRangeHolder == 2)
            {
                Debug.Log("summon enemies chosen");

                abilityOne = false;
                abilityTwo = true;
                abilityThree = false;
                abilityFour = false;

                walk = false;
            }
            else if (randomRangeHolder == 3)
            {
                Debug.Log("spits blood chosen");

                abilityOne = false;
                abilityTwo = false;
                abilityThree = true;
                abilityFour = false;

                walk = false;
            }
            else if (randomRangeHolder == 4)
            {
                Debug.Log("runs at the player and slams chosen");

                abilityOne = false;
                abilityTwo = false;
                abilityThree = false;
                abilityFour = true;

                walk = false;
            }
            else
            {
                Debug.Log("wtf");
            }

            randomRangeHolder = 0;
        }












        //flails at the player 
        if (abilityOne == true)
        {
            if (onlyOnce == false)
            {
                resetHolder = resetTime + Time.time;
                onlyOnce = true;
            }
        }

        //rips something out of the background and throws it at the player
        if (abilityTwo == true)
        {
            if (onlyOnce == false)
            {
                resetHolder = resetTime + Time.time;
                onlyOnce = true;
            }

        }


        //stomps on the ground causing a big rock to come out and throws it at the player 
        if (abilityThree == true)
        {
            if (onlyOnce == false)
            {
                resetHolder = resetTime + Time.time;
                onlyOnce = true;
            }

        }


        //runs at the player and slams on the ground when he gets close causing a big shockwave 
        if (abilityFour == true)
        {
            if (onlyOnce == false)
            {
                Debug.Log("working");
                runsAtPlayerOnlyOnce = true;
                resetHolder = resetTime + Time.time;
                onlyOnce = true;
            }

            if (runsAtPlayerOnlyOnce == true)
            {
                Debug.Log("working");
                if (transform.position.x < player.transform.position.x)
                {
                    rb.velocity = new Vector2(runsAtPlayerSpeed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(-runsAtPlayerSpeed, rb.velocity.y);
                }
            }

            if (transform.position.x > player.transform.position.x - 1)
            {
                if (transform.position.x < player.transform.position.x + 1)
                {
                    if (runsAtPlayerOnlyOnceTwo == false)
                    {
                        runsAtPlayerOnlyOnce = false;

                        //play slamsOnGround Animation
                        Debug.Log("slams Down Animation Plays Here");
                        runsAtPlayerOnlyOnceTwo = true;
                    }
                }

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

            runsAtPlayerOnlyOnceTwo = false;


            walk = true;

            abilityOne = false;
            abilityTwo = false;
            abilityThree = false;
            abilityFour = false;


            randomHolder = walkTime + Time.time;
            onlyOnce = false;
            resetBoss = true;
        }
        if (GetComponent<Health>().currentHealth <= 0)
        {
            //play next stage animation


            //set stage
            Debug.Log("stage three has begun");
            GetComponent<b2S3>().enabled = true;
            GetComponent<b2S2>().enabled = false;
        }


    }
}