using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss4S3 : MonoBehaviour
{
    //stage 3 - lady half turns ashe black and red eyes and still has spider half
    //swings on a spider web and slams down
    private bool swings;
    public GameObject spiderWebThree;




    //fangs strech out of the base of the spider and try to chomp the player
    private bool fangs;




    //leaps up and shoots a web at the player which falls in the direction she shot from
    private bool leaps;
    private float pRight;
    private float initialHolder;
    private float leapHolder;
    public float initialRunSpeed;
    public float initialDurration;
    public float passAmount;
    public float leapJumpForce;
    public float leapSpeed;
    public float leapDurration;
    public GameObject spiderWebFour;





    //venom shoots out of the spiders mouth
    private bool venom;
    private float jumpTimeHolder;
    private float dashTimeHolder;
    private float dashDurrationHolder;
    public GameObject venomGlob;
    public float jumpHeight;
    public float jumpTime;
    public float timeB4HitsGround;
    public float dashSpeed;
    public float dashDurration;




    //walks around between abilities
    private bool walk;
    public float moveSpeed;
    public float walkDurration;





    //general stuff
    private bool onlyOnce;
    private float holder;
    private float resetHolder;
    private Rigidbody2D rb;
    private GameObject player;

    void Start()
    {

        Debug.Log("stage 3 begun");
        rb = GetComponent<Rigidbody2D>();


        GetComponent<Health>().GainHealth(-1);

        ResetBoss();
        //swings = true;
        //fangs = true;
        //leaps = true;
        //venom = true;



    }

    void Update()
    {
        player = GameObject.FindWithTag("Player");






        //reset
        if (resetHolder != 0)
        {
            if (resetHolder < Time.time)
            {
                holder = Random.Range(1, 5);
                resetHolder = 0;
            }
        }

        if (holder != 0)
        {
            if (holder == 1)
            {
                Debug.Log("swings chosen");
                swings = true;    
                fangs = false;
                leaps = false;
                venom = false;
                walk = false;
            }
            else if (holder == 2)
            {
                Debug.Log("fangs chosen");
                swings = false;
                fangs = true;
                leaps = false;
                venom = false;
                walk = false;

            }
            else if (holder == 3)
            {
                Debug.Log("leaps chosen");
                swings = false;
                fangs = false;
                leaps = true;
                venom = false;
                walk = false;

            }
            else if (holder == 4)
            {
                Debug.Log("venom chosen");
                swings = false;
                fangs = false;
                leaps = false;
                venom = true;
                walk = false;

            }
            else
            {
                Debug.Log("wtf");
            }
            holder = 0;
        }









        //abilities 
        if (swings == true)
        {
            SwingsWeb();
        }
        if (fangs == true) 
        {
            FangsPlayer();
        }
        if (leaps == true)
        {
            LeapsAt();
        }
        if (venom == true)
        {
            VenomSpew();
        }
        if (walk == true)
        {
            WalksAround();
        }

        if (GetComponent<Health>().currentHealth <= 0)
        {
            BossDies();
        }
    }





   



   



   
    //swings on a spider web and slams down
    void SwingsWeb()
    {
        if(onlyOnce == false)
        {
            Debug.Log("needs animation");
            onlyOnce = true;
            ResetBoss();
        }
    }




    //fangs strech out of the base of the spider and try to chomp the player
    void FangsPlayer()
    {
        if (onlyOnce == false)
        {
            Debug.Log("needs animation");
            onlyOnce = true;
            ResetBoss();
        }
    }



    //leaps up and shoots a web at the player which falls in the direction she shot from
    void LeapsAt()
    {
        if (onlyOnce == false)
        {
            
            if (transform.position.x < player.transform.position.x)
            {
                Instantiate(spiderWebFour, new Vector2(transform.position.x + 1, transform.position.y + 1), transform.rotation);
                rb.velocity = new Vector2(initialRunSpeed, rb.velocity.y);
                pRight = 1;
                //one means player is on the right 2 means player is on the left
            }
            else
            {
                Instantiate(spiderWebFour, new Vector2(transform.position.x - 1, transform.position.y + 1), transform.rotation);
                rb.velocity = new Vector2(-initialRunSpeed, rb.velocity.y);
                pRight = 2;
                //one means player is on the right 2 means player is on the left
            }

            onlyOnce = true;
        }

        if (pRight != 0)
        {
            if(pRight == 2)
            {
                if (transform.position.x < player.transform.position.x - passAmount)
                {
                    initialHolder = Time.time + initialDurration;
                    Debug.Log("working");
                    pRight = 0;
                }
            }
            if(pRight == 1)
            {
                if (transform.position.x > player.transform.position.x + passAmount)
                {
                    initialHolder = Time.time + initialDurration;
                    Debug.Log("working");
                    pRight = 0;
                }
            }
        }
        if (initialHolder != 0)
        {
            if (initialHolder < Time.time)
            {
                if (transform.position.x < player.transform.position.x)
                {
                    rb.velocity = new Vector2(leapSpeed, leapJumpForce);
                }
                else
                {
                    rb.velocity = new Vector2(-leapSpeed, leapJumpForce);
                }
                leapHolder = Time.time + leapDurration;
                initialHolder = 0;
            }
        }
        
        if (leapHolder != 0)
        {
            if (leapHolder < Time.time)
            {
                rb.velocity = new Vector2(0, 0);

                leapHolder = 0;
                ResetBoss();

            }
        }
    }









    //venom shoots out of the spiders mouth
    void VenomSpew()
    {
        if(onlyOnce == false)
        {

            rb.velocity = new Vector2(0, jumpHeight);
            
            jumpTimeHolder = Time.time + jumpTime;


            onlyOnce = true;
        }

        if (jumpTimeHolder != 0) 
        {
            if (jumpTimeHolder < Time.time)
            {
                rb.velocity = new Vector2(0,0);
                if (transform.position.x < player.transform.position.x)
                {
                    Instantiate(venomGlob, new Vector2(transform.position.x + 2, transform.position.y), transform.rotation);
                }
                else
                {
                    Instantiate(venomGlob, new Vector2(transform.position.x - 2, transform.position.y), transform.rotation);
                }
                dashTimeHolder = Time.time + timeB4HitsGround;
                jumpTimeHolder = 0;
            }
        }

        if(dashTimeHolder != 0)
        {
            if(dashTimeHolder < Time.time)
            {
                if (transform.position.x < player.transform.position.x)
                {
                    rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(-dashSpeed, rb.velocity.y);
                }
                dashTimeHolder = 0;
                dashDurrationHolder = dashDurration + Time.time;
            }
        }


        if(dashDurrationHolder != 0)
        {
            if(dashDurrationHolder < Time.time)
            {
                rb.velocity = new Vector2(0,0);
                ResetBoss();
            }
        }

    }




    // between abilities the boss needs to do something special
    void WalksAround()
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




    //reset boss
    void ResetBoss()
    {
        Debug.Log("boss reset");
        onlyOnce = false;
        walk = true;
        resetHolder = walkDurration + Time.time;


        swings = false; 
        fangs = false;
        leaps = false;
        venom = false;
        CancelInvoke();
    }


    void BossDies()
    {
        //play animation - death animation

        //Boss dies
        Destroy(gameObject, 0);

    }
}