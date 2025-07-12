using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b2S1 : MonoBehaviour
{
    //when he gets lower on health he takes less damage and does more damage
    //                                                     stage one - ninja with oni mask (small funny looking not too intemidating) has chain flail thingies 
    private bool stageOne;


    //dash through the player drops his mask when dashing it flies in the oposite direction of the dash then he jumps back to get it 
    private bool abilityOne;
    public float s1DashThroughResetTime;
    public GameObject mask;
    public Transform barrelEnd;
    private bool dashThroughOnlyOnce;
    private bool happensAfter;
    private float turnBackHolder;
    public float dashThroughSpeed;
    public float turnBackSpeed;
    public float turnBackTime;
    public float runBackSpeed;





    //throw 3 ninja stars at the player
    private bool abilityTwo;
    private float ninjaStarJumpHolder;
    public float ninjaStarJumpForce;
    public float ninjaStarJumpTime;
    public float ninjaStarResetTime;
    public Transform leftBarrelEnd;
    public Transform rightBarrelEnd;
    public GameObject ninjaStar;


    //shoots out the flail thing and reels it back in
    private bool abilityThree;



    //jump up and slams down quickly with both flails with a long range
    private bool abilityFour;


    //walking
    private bool stageOneWalk;
    public float stageOneWalkTime;
    public float walkSpeed;



    //important variables
    private bool onlyOnce;
    private bool resetBoss;
    private Vector2 moveDirection;
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
        stageOne = true;
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
                Debug.Log("dash through the player chosen");

                abilityOne = true;
                abilityTwo = false;
                abilityThree = false;
                abilityFour = false;

                stageOneWalk = false;
            }
            else if (randomRangeHolder == 2)
            {
                Debug.Log("throw 3 ninja stars chosen");

                abilityOne = false;
                abilityTwo = true;
                abilityThree = false;
                abilityFour = false;

                stageOneWalk = false;
            }
            else if (randomRangeHolder == 3)
            {
                Debug.Log("shoots out the flail");

                abilityOne = false;
                abilityTwo = false;
                abilityThree = true;
                abilityFour = false;

                stageOneWalk = false;
            }
            else if (randomRangeHolder == 4)
            {
                Debug.Log("jump up and slams down chosen");

                abilityOne = false;
                abilityTwo = false;
                abilityThree = false;
                abilityFour = true;

                stageOneWalk = false;
            }
            else
            {
                Debug.Log("wtf");
            }

            randomRangeHolder = 0;
        }


        //dash through the player drops his mask when dashing it flies in the oposite direction of the dash then he jumps back to get it 
        if (abilityOne == true)
        {
            if (onlyOnce == false)
            {
                moveDirection = (player.transform.position - transform.position).normalized * dashThroughSpeed;
                rb.velocity = new Vector2(moveDirection.x, moveDirection.y);

                turnBackHolder = Time.time + turnBackTime;

                onlyOnce = true;
            }

            if (turnBackHolder != 0)
            {
                if (turnBackHolder < Time.time)
                {
                    happensAfter = true;
                    moveDirection = (mask.transform.position - transform.position).normalized * turnBackSpeed;
                    rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
                    resetHolder = resetTime + Time.time;

                    turnBackHolder = 0;
                }
            }

            if (dashThroughOnlyOnce == false)
            {
                if (transform.position.x > player.transform.position.x - 1)
                {
                    if (transform.position.x < player.transform.position.x + 1)
                    {
                        happensAfter = false;
                        Instantiate(mask, barrelEnd.transform.position, barrelEnd.transform.rotation);
                        dashThroughOnlyOnce = true;
                    }
                }
            }
        }



        //throw 3 ninja stars at the player
        if (abilityTwo == true)
        {
            if (onlyOnce == false)
            {
                rb.AddForce(Vector2.up * ninjaStarJumpForce, ForceMode2D.Impulse);
                ninjaStarJumpHolder = ninjaStarJumpTime + Time.time;
                resetHolder = ninjaStarResetTime + Time.time;
                onlyOnce = true;
            }
            if (ninjaStarJumpHolder != 0)
            {
                if (ninjaStarJumpHolder < Time.time)
                {
                    rb.velocity = new Vector2(0, 0);
                    if (transform.position.x < player.transform.position.x)
                    {
                        Instantiate(ninjaStar, new Vector2(rightBarrelEnd.transform.position.x, rightBarrelEnd.transform.position.y - 2), barrelEnd.transform.rotation);
                        Instantiate(ninjaStar, new Vector2(rightBarrelEnd.transform.position.x, rightBarrelEnd.transform.position.y + 2), barrelEnd.transform.rotation);
                        Instantiate(ninjaStar, new Vector2(rightBarrelEnd.transform.position.x, rightBarrelEnd.transform.position.y), barrelEnd.transform.rotation);
                    }
                    else
                    {
                        Instantiate(ninjaStar, new Vector2(leftBarrelEnd.transform.position.x, leftBarrelEnd.transform.position.y - 2), barrelEnd.transform.rotation);
                        Instantiate(ninjaStar, new Vector2(leftBarrelEnd.transform.position.x, leftBarrelEnd.transform.position.y + 2), barrelEnd.transform.rotation);
                        Instantiate(ninjaStar, new Vector2(leftBarrelEnd.transform.position.x, leftBarrelEnd.transform.position.y), barrelEnd.transform.rotation);
                    }
                    ninjaStarJumpHolder = 0;
                }
            }

        }


        //shoots out the flail thing and reels it back in
        if (abilityThree == true)
        {
            if (onlyOnce == false)
            {
                resetHolder = resetTime + Time.time;
                onlyOnce = true;
            }

        }


        //jump up and slams down quickly with both flails with a long range
        if (abilityFour == true)
        {
            if (onlyOnce == false)
            {
                resetHolder = resetTime + Time.time;
                onlyOnce = true;
            }

        }





        //normal walk
        if (stageOneWalk == true)
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
            dashThroughOnlyOnce = false;


            stageOneWalk = true;

            abilityOne = false;
            abilityTwo = false;
            abilityThree = false;
            abilityFour = false;


            randomHolder = stageOneWalkTime + Time.time;
            onlyOnce = false;
            resetBoss = true;
        }
        if (GetComponent<Health>().currentHealth <= 0)
        {
            //play next stage animation



            //set stage
            stageOne = false;
            GetComponent<b2S2>().enabled = true;
            GetComponent<b2S1>().enabled = false;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (stageOne == true)
        {
            if (happensAfter == true)
            {
                if (collision.gameObject.tag == "b2OniMask")
                {
                    if (transform.position.x < mask.transform.position.x)
                    {
                        rb.velocity = new Vector2(-runBackSpeed, 0);
                    }
                    else
                    {
                        rb.velocity = new Vector2(runBackSpeed, 0);
                    }
                    resetHolder = s1DashThroughResetTime + Time.time;
                    Destroy(GameObject.FindWithTag("b2OniMask"), 0);
                    happensAfter = false;
                }
            }
        }
    }

}
