using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBoss4 : MonoBehaviour
{


    private bool noCast;
    private bool lick;
    private bool wind;
    private bool rain;

    //noCast variables
    public float floatSpeed;
    public GameObject dripFromUmbrella;



    //lick variables



    //wind variables
    private float windHolder;


    public float windForce;
    public float windAttackDurration;
    public float shootDelay;

    public GameObject sboss4Bullet;
    public GameObject spikeWall;
    public Transform mid;
    public Transform right;
    public Transform left;




    //rain variables


    public float rainDurration;
    public GameObject rainDropSpawner;



    private float resetHolder;
    private float holder;
    private bool onlyOnce;


    public float resetDelay;
    GameObject player;
    Rigidbody2D playerRb;
    Rigidbody2D rb;

    //healthbar stuff
    private bool healthOnlyOnce;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        resetBoss();
        
    }

    void Update()
    {
        if (resetHolder != 0)
        {
            if(resetHolder < Time.time)
            {
                holder = Random.Range(1, 4);
                resetHolder = 0;
            }



            if(holder != 0)
            {
                if (holder == 1)
                {
                    wind = true;
                    lick = false;
                    rain = false;
                    noCast = false;
                    onlyOnce = false;
                    dripFromUmbrella.SetActive(false);
                    holder = 0;
                }
                else if (holder == 2)
                {
                    wind = false;
                    lick = true;
                    rain = false;
                    noCast = false;
                    onlyOnce = false;
                    dripFromUmbrella.SetActive(false);
                    holder = 0;
                }
                else if (holder == 3)
                {
                    wind = false;
                    lick = false;
                    rain = true;
                    noCast = false;
                    onlyOnce = false;
                    dripFromUmbrella.SetActive(false);
                    holder = 0;
                }
                else
                {
                    Debug.Log("WTF");
                }
            }



            
        }



        

        player = GameObject.FindWithTag("Player");

        if (noCast == true)
        {
            NoCast();
        }

        if (wind == true)
        {
            WindAttack();
        }

       if(lick == true)
        {
            LickAttack();
        }

       if(rain == true)
        {
            RainAttack();
        }



        //when the boss has no more health
        if (GetComponent<Health>().currentHealth <= 0)
        {
            GameObject[] minions = GameObject.FindGameObjectsWithTag("SBoss4Minions");
            foreach (GameObject minion in minions)
                Destroy(minion);

            BossDies();
        }
    }








    //normal mode of the boss
    void NoCast()
    {

        //make a drip when walking
        if(onlyOnce == false)
        {
            dripFromUmbrella.SetActive(true);
            onlyOnce = true;
        }



        if (transform.position.x < player.transform.position.x)
        {
            rb.velocity = new Vector2(floatSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-floatSpeed, rb.velocity.y);
        }
    }



    // Vicious lick 
    void LickAttack()
    {
        //need animation to be able to properly position the character

        if(onlyOnce == false)
        {
            Debug.Log("lick attack selected but not yet created");
            onlyOnce = true;
        }
        Invoke("resetBoss", 2);
    }




    // he blows the player back with high winds 
    void WindAttack()
    {
        if(onlyOnce == false)
        {
            Instantiate(spikeWall, right.position, right.rotation);
            Instantiate(spikeWall, left.position, left.rotation);

            Invoke("resetBoss", windAttackDurration);

            
            onlyOnce = true;



        }
        if(windHolder < Time.time)
            {
            Instantiate(sboss4Bullet, mid.position, mid.rotation);
            windHolder = Time.time + shootDelay;

            }



        playerRb = player.GetComponent<Rigidbody2D>();

        if (transform.position.x < player.transform.position.x)
        {
            playerRb.velocity = new Vector2(windForce, playerRb.velocity.y);
        }
        else
        {
            playerRb.velocity = new Vector2(-windForce, playerRb.velocity.y);
        }



    }




    // Rain attack
    void RainAttack()
    {
        if(onlyOnce == false)
        {
            Instantiate(rainDropSpawner, transform.position, transform.rotation);
            Invoke("resetBoss", rainDurration);
            onlyOnce = true;
            

        }
    }

    void resetBoss()
    {
        Debug.Log("boss reset");


        GameObject[] minions = GameObject.FindGameObjectsWithTag("SBoss4Minions");
        foreach (GameObject minion in minions)
            Destroy(minion);


        noCast = true;
        lick = false;
        wind = false;
        rain = false;

        
        resetHolder = Time.time + resetDelay;
        CancelInvoke();

    }







    //heath related
    void BossDies()
    {
        //play die animation

        //destroy
        Destroy(gameObject, 0);

    }

}
