using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b5S3 : MonoBehaviour
{//Boss 5 - mimikyu (copy the things it sees in a creepier way tho)
    //stage 3 - transforms back into its origonal form which is a black reaper looking thing with a sythe but has an unerving smile and blacked out eyes







    //shoots lots of projectiles that the player has to dodge
    private bool shoots;



    //lazers dark magic
    private bool lazers;



    //slashes very quick with its sythe
    private bool slashes;



    //flaming skull that bounces off the ground and leaves the boss headless 
    private bool flaming;



    //walk 
    private bool walk;
    public float walkDurration;
    public float moveSpeed;



    //general stuff
    private bool onlyOnce;
    private float holder;
    private float resetHolder;
    private Rigidbody2D rb;
    private GameObject player;


    void Start()
    {
        GetComponent<Health>().GainHealth(-1);
        rb = GetComponent<Rigidbody2D>();


        ResetBoss();
        //shoots = true;
        //lazers = true;
        //slashes = true;
        //flaming = true;
    }




    void Update()
    {
        player = GameObject.FindWithTag("Player");




        
        if (GetComponent<Health>().currentHealth <= 0)
        {
            BossDies();
        }





        //ability picker
        if (resetHolder != 0)
        {
            if (resetHolder < Time.time)
            {
                holder = Random.Range(1, 5);
                resetHolder = 0;
            }
        }
        if (holder == 1)
        {
            Debug.Log("Penanggalan chosen");
            shoots = true;   
            lazers = false;
            slashes = false;
            flaming = false;
            walk = false;
        }
        else if (holder == 2)
        {
            Debug.Log("oni chosen");
            shoots = false;
            lazers = true;
            slashes = false;
            flaming = false;
            walk = false;
        }
        else if (holder == 3)
        {
            Debug.Log("dragon chosen");
            shoots = false;
            lazers = false;
            slashes = true;
            flaming = false;
            walk = false;
        }
        else if (holder == 4)
        {
            Debug.Log("Jorogumo chosen");
            shoots = false;
            lazers = false;
            slashes = false;
            flaming = true;
            walk = false;

        }
        holder = 0;


        //abilities
        if (shoots == true)
        {
            shootsLots();
        }
        if (lazers == true)    
        {
            lazerMagic();
        }
        if (slashes == true)
        {
            slashesSythe();

        }
        if (flaming == true)
        {
            flamingSkull();
        }
        if (walk == true)
        {
            WalksAround();
        }
    }









    //shoots lots of projectiles that the player has to dodge
    void shootsLots()
    {
        if (onlyOnce == false)
        {
            Debug.Log("not made yet");
            onlyOnce = true;
            ResetBoss();
        }
    }








    //lazers dark magic
    void lazerMagic()
    {
        if (onlyOnce == false)
        {
            Debug.Log("not made yet");
            onlyOnce = true;
            ResetBoss();
        }
    }








    //slashes very quick with its sythe
    void slashesSythe()
    {
        if (onlyOnce == false)
        {
            Debug.Log("not made yet");
            onlyOnce = true;
            ResetBoss();
        }
    }







    //flaming skull that bounces off the ground and leaves the boss headless 
    void flamingSkull()
    {
        if (onlyOnce == false)
        {
            Debug.Log("not made yet");
            onlyOnce = true;
            ResetBoss();
        }
    }








    // between abilities the boss needs to do something special
    void WalksAround()
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








    //reset boss
    void ResetBoss()
    {
        Debug.Log("boss reset");
        onlyOnce = false;
        walk = true;
        resetHolder = walkDurration + Time.time;


        shoots = false;
        lazers = false;
        slashes = false;
        flaming = false;
        CancelInvoke();
    }






    //heath related
    void BossDies()
    {
        //play animation - the skull returns to the hood of the reaper and he screams out all his eccesnse then his cloak and sythe fall to the ground


        //he dead
        Destroy(gameObject, 0);

    }
}