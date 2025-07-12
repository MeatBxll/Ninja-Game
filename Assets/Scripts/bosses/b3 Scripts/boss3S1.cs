using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss3S1 : MonoBehaviour
{
    //stage one and two are the same where the dragon is switching between being airborn and landing on the ground turning half human half dragon doing ground attacks with a lot of heath shared on the two 


                            //dragon form
    private bool dragon;
    private bool dragonOnce;
    private float dragonHolder;
    public float dragonStartJump;
    public float dragonStartJumpTime;


    //dragon fly
    private bool dragonFly;
    public float dragonFlySpeed;






                            //human form
    private bool human;
    private bool humanOnce;
    private bool humanHolder;
    private float humanHolder2;
    public float humanFallForce;
    public float rollTime;


    //Human walk
    private bool humanWalk;
    public float humanWalkSpeed;



    //variables for both
    private bool reset;
    private float switchHolder;
    public float timeBetweenSwitches;


    //set next stage variables
    private float dragonHumanFullTransform;


    //important game objects 
    private GameObject player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dragon = true;
    }





    private void Update()
    {
        player = GameObject.FindWithTag("Player");
        if(dragon == true)
        {
            DragonForm();
        }

        if(human == true)
        {
            HumanForm();
        }

        if (GetComponent<Health>().currentHealth <= 0)
        {
            BossDies();
        }

    }



    void DragonForm()
    {
        //dragon form start off
        if (dragonOnce == false)
        {
            // play dragon swap animation but do not full stop the boss must be a moving animation


            //dragon mode setup
            Debug.Log("Dragon switch");
            rb.gravityScale = 0;
            dragonOnce = true;
            reset = true;
            dragonFly = false;


            //setup time between switches
            switchHolder = timeBetweenSwitches + Time.time;


            //dragon setup jump
            rb.AddForce(Vector2.up * dragonStartJump, ForceMode2D.Impulse);

            if (transform.position.x < player.transform.position.x)
            {
                rb.AddForce(Vector2.right * dragonStartJump/2, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.left * dragonStartJump/2, ForceMode2D.Impulse);
            }
            dragonHolder = dragonStartJumpTime + Time.time;
            
        }
        
        if(dragonHolder != 0)
        {
            if(dragonHolder < Time.time)
            {
                rb.velocity = new Vector2(0, 0);
                dragonHolder = 0;
                reset = false;
            }
        }


        //switch holder
        if(switchHolder < Time.time)
        {
            dragon = false;
            human = true;
            dragonOnce = false;
        }












        //dragon fly
        if(dragonFly == true)
        {
            if (transform.position.x < player.transform.position.x)
            {
                rb.velocity = new Vector2(dragonFlySpeed, rb.velocity.y);
            }
            else
            { 
                rb.velocity = new Vector2(-dragonFlySpeed, rb.velocity.y);
            }
        }



        //reset
        if(reset == false)
        {
            dragonFly = true;
        }

    }












    void HumanForm()
    {
        if(humanOnce == false)
        {
            //human mode setup
            Debug.Log("Human switch");
            rb.gravityScale = 1;
            humanOnce = true;
            reset = true;
            humanWalk = false;


            //setup time between switches
            switchHolder = timeBetweenSwitches + Time.time;


            //human setup fall and glide
            rb.AddForce(Vector2.down * humanFallForce, ForceMode2D.Impulse);
            humanHolder = true;

        }

        if (humanHolder2 != 0)
        {
            if (humanHolder2 < Time.time)
            {
                rb.velocity = new Vector2(0, 0);
                humanHolder2 = 0;
                reset = false;
            }
        }

        //switch holder
        if (switchHolder < Time.time)
        {
            dragon = true;
            human = false;
            humanOnce = false;
        }









        //human Walk
        if (humanWalk == true)
        {
            if (transform.position.x < player.transform.position.x)
            {
                rb.velocity = new Vector2(humanWalkSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-humanWalkSpeed, rb.velocity.y);
            }
        }



        //reset
        if (reset == false)
        {
            humanWalk = true;
        }
    }


    








    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(humanHolder == true)
        {
            //he rolls towards the player

            if (transform.position.x < player.transform.position.x)
            {
                rb.AddForce(Vector2.right * humanFallForce, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.left * humanFallForce, ForceMode2D.Impulse);
            }
            humanHolder2 = Time.time + rollTime;
            humanHolder = false;
        }
    }












    void BossDies()
    {
        
        dragonHumanFullTransform = Random.Range(1,3);
        if(dragonHumanFullTransform == 1)
        {
            GetComponent<boss3FullDragon>().enabled = true;
            GetComponent<boss3S1>().enabled = false;
        }
        else
        {
            GetComponent<boss3FullHuman>().enabled = true;
            GetComponent<boss3S1>().enabled = false;
        }

    }
}
