using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss3FullHuman : MonoBehaviour
{
    private bool abilityOne;
    public float slashSpeed;
    public float slashDurration;

    private bool abilityTwo;
    public float abilityTwoResetTime;
    public GameObject tornado;

    private bool abilityThree;
    public float abilityThreeResetTime;
    public float dashSpeed;
    public float dashDurration;

    private bool chasePlayer;
    public float moveSpeed;
    public float chaseDurration;


    //setup stuff
    private bool setupOnlyOnce;
    private bool humanFormCollision;
    public float setupDownSpeed;
    public float animationDurration;
    


    //overall variables
    private bool onlyOnce;
    private float resetHolder;
    private float randomHolder;


    //important game objects 
    private GameObject player;
    private Rigidbody2D rb;


    void Start()
    {
        Debug.Log("fullHuman picked");

        //healthBar Start
        GetComponent<Health>().GainHealth(-1);

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        BossSetup();

        humanFormCollision = true;
    }


    void Update()
    {
        player = GameObject.FindWithTag("Player");



        //Damage and health
        if (GetComponent<Health>().currentHealth <= 0)
        {
            BossDies();
        }




        // randomizer 
        if (resetHolder != 0)
        {
            if (resetHolder < Time.time)
            {
                randomHolder = Random.Range(1, 4);
                resetHolder = 0;
            }
        }


        if (randomHolder != 0)
        {
            if (randomHolder == 1)
            {
                abilityOne = true;
                abilityTwo = false;
                abilityThree = false;
                chasePlayer = false;
                Debug.Log("simple slash chosen");
            }
            if (randomHolder == 2)
            {
                abilityOne = false;
                abilityTwo = true;
                abilityThree = false;
                chasePlayer = true;
                Debug.Log("throw tornado chosen");
            }
            if (randomHolder == 3)
            {
                abilityOne = false;
                abilityTwo = false;
                abilityThree = true;
                chasePlayer = false;
                Debug.Log("dash into rapid attacks chosen");
            }
            randomHolder = 0;
        }


        if (abilityOne == true)
        {
            AbilityOne();
        }
        if (abilityTwo == true)
        {
            AbilityTwo();
        }
        if (abilityThree == true)
        {
            AbilityThree();
        }
        if (chasePlayer == true)
        {
            ChasePlayer();
        }



    }





    void AbilityOne()
    {
        //simple slash towards the player
        if (onlyOnce == false)
        {
            if (transform.position.x < player.transform.position.x)
            {
                rb.velocity = new Vector2(slashSpeed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-slashSpeed, 0);
            }

            Invoke("AbilityOneStageTwo", slashDurration);
            onlyOnce = true;


        }

    }

    void AbilityOneStageTwo()
    { 
        Debug.Log("working");
        rb.velocity = new Vector2(0, 0);
        BossReset();
        CancelInvoke();
    }









    void AbilityTwo()
    {
        //shoot tornado at player
        if (onlyOnce == false)
        {
            Invoke("BossReset", abilityTwoResetTime);

            if (transform.position.x < player.transform.position.x)
            {
                Instantiate(tornado, new Vector2(transform.position.x + 3, transform.position.y), transform.rotation);
            }
            else
            {
                Instantiate(tornado, new Vector2(transform.position.x - 3, transform.position.y), transform.rotation);
            }
            onlyOnce = true;
        }


    }








    void AbilityThree()
    {
        //dashes in and does rapid attacks
        if (onlyOnce == false)
        {
            if (transform.position.x < player.transform.position.x)
            {
                rb.velocity = new Vector2(dashSpeed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-dashSpeed, 0);
            }
            
            Invoke("AbilityThreeStageTwo", dashDurration);
            onlyOnce = true;
        }

    }

    void AbilityThreeStageTwo()
    {
        rb.velocity = new Vector2(0, 0);
        BossReset();
        CancelInvoke();
    }




    void ChasePlayer()
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




    void BossReset()
    {
        //abilities reset if needed



        //actual abilities disabled;
        abilityOne = false;
        abilityTwo = false;
        abilityThree = false;


        //Reset Usual
        Debug.Log("resetBoss");
        rb.velocity = new Vector2(0, 0);
        onlyOnce = false;
        chasePlayer = true;
        resetHolder = chaseDurration + Time.time;
        CancelInvoke();
    }









    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(humanFormCollision == true)
        {
            if (setupOnlyOnce == false)
            {
                rb.velocity = new Vector2(0, 0);
                setupOnlyOnce = true;
                BossReset();
                rb.gravityScale = 1;
            }



        }

    }






    void BossSetup()
    {
        //play setup animation



        //change Durration to the length of the stage animaition
        Invoke("BossSetupStageTwo", animationDurration);
        
    }

    void BossSetupStageTwo()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y+1);
        rb.velocity = new Vector2(0, -setupDownSpeed);
        CancelInvoke();
    }



    void BossDies()
    {
        // play death animation where the dragon eats itself starting at the tail


        Destroy(gameObject);

    }
}
