using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss3FullDragon : MonoBehaviour
{
    private bool abilityOne;
    public float abilityOneResetTime;

    private bool abilityTwo;
    private float flyByHolder;
    public float abilityTwoResetTime;
    public GameObject fireBreath;
    public float flyToSpeed;
    public float flyBySpeed;
    public float heightOffGround;
    public float flyAwayDistance;

    private bool abilityThree;
    private bool fliesAtPlayerOnlyOnce;
    public float abilityThreeResetTime;
    public float fliesAtGroundSpeed;
    public float flyAtPlayerSpeed;
    public float flyAtPlayerDurration;

    private bool chasePlayer;
    public float moveSpeed;
    public float chaseDurration;



    //setup variables
    private bool dragonFormCollision;
    private bool setupOnce;
    public float animationDurration;
    public float setupSlam;
    public float jumpSpeed;
    public float jumpDurration;


    //overall variables
    private Vector2 moveDirection;
    private bool onlyOnce;
    private float resetHolder;
    private float randomHolder;


    //important game objects 
    private GameObject player;
    private Rigidbody2D rb;


    void Start()
    {
        Debug.Log("fullDragon picked");
        //healthBar Start
        GetComponent<Health>().GainHealth(-1);

        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(0, 0);
        BossSetup();
        dragonFormCollision = true;
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
        if(resetHolder != 0)
        {
            if(resetHolder < Time.time)
            {
                randomHolder = Random.Range(1, 4);
                resetHolder = 0;
            }
        }


        if(randomHolder != 0)
        {
            if (randomHolder == 1)
            {
                abilityOne = true;
                abilityTwo = false;
                abilityThree = false;
                chasePlayer = false;
                Debug.Log("flow into ground chosen");
            }
            if (randomHolder == 2)
            {
                abilityOne = false;
                abilityTwo = true;
                abilityThree = false;
                chasePlayer = false;
                Debug.Log("flies with fire breath chosen");
            }
            if (randomHolder == 3)
            {
                abilityOne = false;
                abilityTwo = false;
                abilityThree = true;
                chasePlayer = false;
                Debug.Log("dashes at the player chosen");
            }
            randomHolder = 0;
        }


        if(abilityOne == true)
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
        if(chasePlayer == true)
        {
            ChasePlayer();
        }



    }





    void AbilityOne()
    {
        //he can go into the ground like water and come up near the player
        if(onlyOnce == false)
        {
            Invoke("BossReset", abilityOneResetTime);
        }
        
    }












    void AbilityTwo()
    {
        //fly by with fire breath
        if (onlyOnce == false)
        {
            moveDirection = (player.transform.position - transform.position).normalized * flyToSpeed;
            flyByHolder = Random.Range(1, 3);
            fireBreath.SetActive(true);
            Invoke("BossReset", abilityTwoResetTime);
        }

        if (flyByHolder == 1)
        {
            rb.linearVelocity = new Vector2(moveDirection.x - flyAwayDistance, moveDirection.y + heightOffGround);
        }
        else
        {
            rb.linearVelocity = new Vector2(moveDirection.x + flyAwayDistance, moveDirection.y + heightOffGround);
        }
    }











    void AbilityThree()
    {
        //flies to the ground and flies towards the player
        if (onlyOnce == false)
        {

            fliesAtPlayerOnlyOnce = true;
            rb.AddForce(Vector2.down * fliesAtGroundSpeed, ForceMode2D.Impulse);
            onlyOnce = true;
        }
        
    }

    void AbilityThreeStageTwo()
    {
        rb.linearVelocity = new Vector2(0, jumpSpeed);
        Invoke("BossReset", jumpDurration);
    }











    void ChasePlayer()
    {
        if (transform.position.x < player.transform.position.x)
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
        }
    }




    void BossReset()
    {
        //abilities use needing reset
        fireBreath.SetActive(false);


        //actual abilities disabled;
        abilityOne = false;
        abilityTwo = false;
        abilityThree = false;


        //Reset Usual
        Debug.Log("resetBoss");
        rb.linearVelocity = new Vector2(0, 0);
        onlyOnce = false;
        chasePlayer = true;
        resetHolder = chaseDurration + Time.time;
        CancelInvoke();
    }







    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(dragonFormCollision == true)
        {
            //setup slam
            if (setupOnce == false)
            {
                rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                Invoke("BossReset", jumpDurration);
                setupOnce = true;
                rb.gravityScale = 0;
            }

            //flies at player
            if (fliesAtPlayerOnlyOnce == true)
            {
                if (transform.position.x < player.transform.position.x)
                {
                    rb.AddForce(Vector2.right * flyAtPlayerSpeed, ForceMode2D.Impulse);
                }
                else
                {
                    rb.AddForce(Vector2.right * -flyAtPlayerSpeed, ForceMode2D.Impulse);
                }
                Invoke("AbilityThreeStageTwo", flyAtPlayerDurration);
                fliesAtPlayerOnlyOnce = false;
            }
        
        }

    }






    void BossSetup()
    {
        //play setup animation





        //change Durration to the length of the stage animaition
        Invoke("BossStart", animationDurration);
    }



    void BossStart()
    {
        rb.AddForce(Vector2.down * setupSlam, ForceMode2D.Impulse);
    }
    










    void BossDies()
    {
        // play death animation where the dragon eats itself starting at the tail


        Destroy(gameObject);

    }
}
