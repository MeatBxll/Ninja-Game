using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBoss2 : MonoBehaviour
{

    private GameObject player;
    private bool chargeAttack;
    private bool slamAttack;
    private bool normalWalk;
    private float halfHealth;
    private float stageTwo;
    private float nextCycle;
    private float randomRange;



    //pin variables 
    private float pinHold;
    private bool onlyOnce;
    private bool playerRightPin;
    private bool playerLeftPin;

    public float fallBackSpeed;
    public float fallBackHeight;
    public float walkDelay;

    //slam variables
    private float slamDelayHolder;

    public float slamHeight;
    public float slamSpeed;
    public float slamDelayAfterJump;






    
    public float stageTwoMultiplier;
    public float agroRange;
    public float moveSpeed;
    public float pinIncreaseAmount;
    public float timeBetweenAbilities;
    public float resetDelay;
    public Transform left;
    public Transform right;


    Rigidbody2D rb;




    //healthBarVariables
    private bool healthOnlyOnce;


    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();

        normalWalk = true;
        nextCycle = 5;

        normalWalk = true;
        nextCycle = 4;

        stageTwo = 1;

    }





    void Update()
    {
        player = GameObject.FindWithTag("Player");
        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (nextCycle != 0)
        {
            if (Time.time > nextCycle)
            {
                randomRange = Random.Range(1, 3);

                //normalWalk
                if (randomRange == 1)
                {
                    Debug.Log("slamPicked");
                    normalWalk = false;
                    chargeAttack = false;
                    slamAttack = true;
                }
                //chargeAttack
                else if (randomRange == 2)
                {
                    Debug.Log("ChargePicked");
                    normalWalk = false;
                    chargeAttack = true;
                    slamAttack = false;
                }
                else
                {
                    Debug.Log("wtf");
                }
                nextCycle = 0;

            }
        }

        


        





        if (normalWalk == true)
        {
            if (distToPlayer < agroRange)
            {
                ChasePlayer();
            }
            else
            {
                StopChasingPlayer();
            }
        }
        








        if (chargeAttack == true)
        {
            if (onlyOnce == false)
            {
                
                if (transform.position.x < player.transform.position.x)
                {
                    transform.position = new Vector2(left.transform.position.x, left.transform.position.y);
                    playerRightPin = true;
                }
                else
                {
                    transform.position = new Vector2(right.transform.position.x, right.transform.position.y);
                    playerLeftPin = true;
                }
                onlyOnce = true;
            }


            if (playerRightPin == true)
            {

                chargeAttackRight();

            }
            if (playerLeftPin == true)
            {
                chargeAttackLeft();
            }

        }




        if (slamAttack == true)
        {
            slamAttackStage1();


        }






        //when the boss has no more health
        if (GetComponent<Health>().currentHealth <= 0)
        {
            BossDies();
        }

        //when boss gets half health
        if (GetComponent<Health>().halfHealth >= GetComponent<Health>().currentHealth)
        {
            if (healthOnlyOnce == false)
            {
                //play stage two animation


                Debug.Log("boss half health stage two started");
                stageTwo = stageTwoMultiplier;
                healthOnlyOnce = true;
            }
        }


    }






    //Charge Attack Stuff
    void chargeAttackRight()
    {
        rb.velocity = new Vector2(pinHold * stageTwo, rb.velocity.y);
        pinHold = pinHold + pinIncreaseAmount;
    }
    void chargeAttackLeft()
    {
        rb.velocity = new Vector2(pinHold * stageTwo, rb.velocity.y);
        pinHold = pinHold - pinIncreaseAmount;
    }









    //slam stuff
    void slamAttackStage1()
    {
        if(onlyOnce == false)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.AddForce(Vector2.up * (slamHeight - stageTwo), ForceMode2D.Impulse);
            slamDelayHolder = Time.time + slamDelayAfterJump;

            
            onlyOnce = true;
        }

        if(slamDelayHolder != 0)
        {
            if(slamDelayHolder < Time.time)
            {
                rb.AddForce(Vector2.down * slamSpeed *stageTwo, ForceMode2D.Impulse);
                slamDelayHolder = 0; 
            }
        }

        Invoke("ResetBoss", resetDelay);

    }










    void OnCollisionEnter2D(Collision2D collision)
    {
        if (chargeAttack == true)
        {
            if (collision.gameObject.tag == "wall")
            {
                
                foreach (ContactPoint2D hitPos in collision.contacts)
                {
                    chargeAttack = false;
                    onlyOnce = true;
                    playerLeftPin = false;
                    playerRightPin = false;
                    pinHold = -1;

                    if (hitPos.normal.x > 0)
                    {
                        rb.velocity = new Vector2(0, 0);
                        rb.AddForce(Vector2.right * fallBackSpeed);
                        rb.AddForce(Vector2.up * fallBackHeight);


                    }
                    else
                    {
                        rb.AddForce(Vector2.left * fallBackSpeed);
                        rb.AddForce(Vector2.up * fallBackHeight);
                    }
                }

                
                Invoke("ResetBoss", resetDelay) ;

            }
        }
    }










    void ChasePlayer()
    {
        if (transform.position.x < player.transform.position.x)
        {
            rb.velocity = new Vector2(moveSpeed * stageTwo, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed * stageTwo, rb.velocity.y);
        }
    }
    void StopChasingPlayer()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }









    void ResetBoss()
    {
        normalWalk = true;
        chargeAttack = false;
        slamAttack = false;
        onlyOnce = false;
        nextCycle = Time.time + timeBetweenAbilities;



        CancelInvoke();
    }

    







    // healthBar stuff
    void TakeDamage(int damage)
    {
        //currentHealth -= damage;

        //healthBar.SetHealth(currentHealth);
    }
    void BossDies()
    {
        //play die animation

        //destroy
        Destroy(gameObject, 0);

    }
}
