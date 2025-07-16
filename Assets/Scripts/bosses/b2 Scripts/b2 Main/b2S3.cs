using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b2S3 : MonoBehaviour
{
    //pulls roots out of the ground up towards the player 
    private bool abilityOne;



    //jumps over and slams down on the player with his body
    private bool abilityTwo;




    //slams down the big branch near the player
    private bool abilityThree;



    //slams down on the ground in front of him making a shockwave go through and jump up at the player
    private bool abilityFour;



    //walk variables
    private bool walk;
    public float walkTime;
    public float walkSpeed;



    //important variables
    private bool onlyOnce;
    private bool resetBoss;
    public float resetTime;
    private float resetHolder;
    private float randomHolder;
    private float randomRangeHolder;



    //important game objects 
    private GameObject player;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetComponent<Health>().GainHealth(-1);
        Debug.Log("stage three has begun");
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
                Debug.Log("pulls roots chosen");

                abilityOne = true;
                abilityTwo = false;
                abilityThree = false;
                abilityFour = false;

                walk = false;
            }
            else if (randomRangeHolder == 2)
            {
                Debug.Log("jumps over and slams chosen");

                abilityOne = false;
                abilityTwo = true;
                abilityThree = false;
                abilityFour = false;

                walk = false;
            }
            else if (randomRangeHolder == 3)
            {
                Debug.Log("stomps towards chosen");

                abilityOne = false;
                abilityTwo = false;
                abilityThree = true;
                abilityFour = false;

                walk = false;
            }
            else if (randomRangeHolder == 4)
            {
                Debug.Log("slams down on the ground chosen");

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












        //pulls roots out of the ground up towards the player
        if (abilityOne == true)
        {
            if (onlyOnce == false)
            {
                resetHolder = resetTime + Time.time;
                onlyOnce = true;
            }
        }

        //jumps over and slams down on the player with his body
        if (abilityTwo == true)
        {
            if (onlyOnce == false)
            {
                resetHolder = resetTime + Time.time;
                onlyOnce = true;
            }

        }


        //stomps towards him slowly creating shockwaves that the player has to jump over
        if (abilityThree == true)
        {
            if (onlyOnce == false)
            {
                resetHolder = resetTime + Time.time;
                onlyOnce = true;
            }

        }


        //slams down on the ground in front of him making a shockwave go through and jump up at the player
        if (abilityFour == true)
        {
            if (onlyOnce == false)
            {
                resetHolder = resetTime + Time.time;
                onlyOnce = true;
            }

        }





        //normal Walking State
        if (walk == true)
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
            BossDies();
        }


    }


    void BossDies()
    {
        //play die animation

        //destroy
        Destroy(gameObject, 0);
    }




}