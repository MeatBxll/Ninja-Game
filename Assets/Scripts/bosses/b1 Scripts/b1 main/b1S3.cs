using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b1S3 : MonoBehaviour
{
    //stage three specials 
    private bool stageThreeOnlyOnce;
    private float stageThreeStartingJumpHolder;
    public float stageThreeStartingJumpForce;
    public float stageThreeStartingJumpTime;
    public Transform stageThreeBarrelEnd;






    //shoots organs then trhe organs run back into the boss
    private bool shootOrgans;
    private bool shootOrgansOnlyOnce;
    private float shootOrgansRandom;
    private float shootOrgansHolder;
    private float shootOrgansHolderTwo;
    public float shootOrgansDurration;
    public float organFireRate;
    public GameObject organOne;
    public GameObject organTwo;
    public GameObject organThree;
    public GameObject organFour;





    //summons enemies ability
    private bool summonEnemies;
    private float summonEnemiesHolder;
    public float summonEnemiesDurration;
    public int summonEnemiesHealAmount;
    public GameObject bloodSeed;






    //spits blood ability
    private bool spitsBlood;
    public float spitsBloodDurration;
    public GameObject bloodPuddle1;
    public GameObject bloodPuddle2;




    //heart ability
    private bool heartAbility;
    public float heartAbilityDurration;




    private bool stageThreeFloat;
    public float floatSpeed;



    //universal things 
    private bool resetBoss;
    private bool onlyOnce;
    private float resetHolder;
    private float randomHolder;
    private float randomRangeHolder;
    public Transform normalBarrelEnd;


    //walk
    private bool stageOneWalk;
    public float stageOneWalkTime;
    public float walkSpeed;


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


        //make animation here that shows the boss dying then put a timer to only start the next part after the animation is over




        //doesnt move until player hits it
        if (stageThreeOnlyOnce == false)
        {
            if (GetComponent<Health>().currentHealth != GetComponent<Health>().maxHealth)
            {
                rb.gravityScale = 0;
                rb.AddForce(Vector2.up * stageThreeStartingJumpForce, ForceMode2D.Impulse);
                stageThreeStartingJumpHolder = Time.time + stageThreeStartingJumpTime;
                stageThreeOnlyOnce = true;
                Debug.Log("stage 3 start thing");
            }
        }

        if (stageThreeStartingJumpHolder != 0)
        {
            if (stageThreeStartingJumpHolder < Time.time)
            {
                resetBoss = false;
                rb.velocity = new Vector2(0, 0);
                stageThreeStartingJumpHolder = 0;
            }
        }




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

                shootOrgans = true;
                summonEnemies = false;
                spitsBlood = false;
                heartAbility = false;

                stageThreeFloat = true;
            }
            else if (randomRangeHolder == 2)
            {
                Debug.Log("summon enemies chosen");

                shootOrgans = false;
                summonEnemies = true;
                spitsBlood = false;
                heartAbility = false;

                stageThreeFloat = true;
            }
            else if (randomRangeHolder == 3)
            {
                Debug.Log("spits blood chosen");

                shootOrgans = false;
                summonEnemies = false;
                spitsBlood = true;
                heartAbility = false;

                stageThreeFloat = false;
            }
            else if (randomRangeHolder == 4)
            {
                Debug.Log("heart ability chosen");

                shootOrgans = false;
                summonEnemies = false;
                spitsBlood = false;
                heartAbility = true;

                stageThreeFloat = false;
            }
            else
            {
                Debug.Log("wtf");
            }

            randomRangeHolder = 0;
        }

        // shoots organs at the player and the organs run underneath where the boss is and jump back into the boss cluster
        if (shootOrgans == true)
        {

            if (onlyOnce == false)
            {
                shootOrgansHolder = 3;

                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

                shootOrgansHolderTwo = shootOrgansDurration + Time.time;
                onlyOnce = true;
            }

            if (shootOrgansHolderTwo < Time.time)
            {
                if (shootOrgansOnlyOnce == false)
                {
                    shootOrgansHolder = 0;
                    shootOrgansRandom = 0;
                    resetHolder = Time.time + 2;
                    shootOrgansOnlyOnce = true;
                }
            }

            if (shootOrgansHolder != 0)
            {
                if (shootOrgansHolder < Time.time)
                {
                    shootOrgansRandom = Random.Range(1, 5);
                    shootOrgansHolder = organFireRate + Time.time;
                }
            }

            if (shootOrgansRandom != 0)
            {
                if (shootOrgansRandom == 1)
                {
                    Instantiate(organTwo, stageThreeBarrelEnd.transform.position, stageThreeBarrelEnd.transform.rotation);
                }
                else if (shootOrgansRandom == 2)
                {
                    Instantiate(organThree, stageThreeBarrelEnd.transform.position, stageThreeBarrelEnd.transform.rotation);
                }
                else if (shootOrgansRandom == 3)
                {
                    Instantiate(organFour, stageThreeBarrelEnd.transform.position, stageThreeBarrelEnd.transform.rotation);
                }
                else
                {
                    Instantiate(organOne, stageThreeBarrelEnd.transform.position, stageThreeBarrelEnd.transform.rotation);
                }

                shootOrgansRandom = 0;
            }
        }




        //summons lots of enemies then later kills all the enemies and heals depending on how many left and if all dies then she takes lots of dmg
        if (summonEnemies == true)
        {
            if (onlyOnce == false)
            {
                Instantiate(bloodSeed, stageThreeBarrelEnd.transform.position, stageThreeBarrelEnd.rotation);
                resetHolder = summonEnemiesDurration + Time.time;
                summonEnemiesHolder = summonEnemiesDurration - 1 + Time.time;
                onlyOnce = true;
            }

            //destroy all enemies and heal off of them
            if (summonEnemiesHolder < Time.time)
            {
                GameObject[] summonEnemiesMinions = GameObject.FindGameObjectsWithTag("b1Minion");
                foreach (GameObject summonEnemiesMinion in summonEnemiesMinions)
                {
                    Debug.Log("working");
                    GetComponent<Health>().GainHealth(summonEnemiesHealAmount);
                    GameObject.Destroy(summonEnemiesMinion);
                }
            }
        }





        //spits blood puddles which cannot be stepped on for a while 
        if (spitsBlood == true)
        {
            if (onlyOnce == false)
            {
                Instantiate(bloodPuddle1, new Vector3(stageThreeBarrelEnd.transform.position.x - 2, stageThreeBarrelEnd.transform.position.y, stageThreeBarrelEnd.transform.position.z), stageThreeBarrelEnd.rotation);
                Instantiate(bloodPuddle2, new Vector3(stageThreeBarrelEnd.transform.position.x + 2, stageThreeBarrelEnd.transform.position.y, stageThreeBarrelEnd.transform.position.z), stageThreeBarrelEnd.rotation);
                resetHolder = spitsBloodDurration + Time.time;
                onlyOnce = true;
            }

        }




        //heart comes out which can take dmg but after a few seconds explodes outwards and shoots guts everywhere 
        if (heartAbility == true)
        {
            if (onlyOnce == false)
            {
                resetHolder = heartAbilityDurration + Time.time;
                onlyOnce = true;
            }

        }





        //normal Walking State
        if (stageThreeFloat == true)
        {
            if (transform.position.x < player.transform.position.x)
            {
                rb.velocity = new Vector2(floatSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-floatSpeed, rb.velocity.y);
            }
        }




        //Reset stage three
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
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            shootOrgansOnlyOnce = false;



            stageThreeFloat = true;

            shootOrgans = false;
            summonEnemies = false;
            spitsBlood = false;
            heartAbility = false;


            randomHolder = stageOneWalkTime + Time.time;
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


        //destroy all the boss minions
        GameObject[] summonEnemiesMinions = GameObject.FindGameObjectsWithTag("b1Minion");
        foreach (GameObject summonEnemiesMinion in summonEnemiesMinions)
        {
            GameObject.Destroy(summonEnemiesMinion);
        }


        //destroy
        Destroy(gameObject, 0);
    }



}
