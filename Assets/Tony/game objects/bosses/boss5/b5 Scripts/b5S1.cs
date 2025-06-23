using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b5S1 : MonoBehaviour
{
    //Boss 5 - mimikyu (copy the things it sees in a creepier way tho)
    ///stage 1 - he trasnforms between the 4 other bosses

    //Penanggalan - he heals a little then spews blood everywhere that the player has to dodge
    private bool pen;



    //oni - he slams down onto the ground then runs at the player creating shockwaves behind him
    private bool oni;



    //Dragon - the dragon form shoots a fireball then transforms into ninja form and dashes at the player
    private bool drag;



    //Jorogumo - lays some spider childern and shoots 3 webs that the player cant go through unless they shoot something at it to break it
    private bool joro;



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

        rb = GetComponent<Rigidbody2D>();
        


        ResetBoss();
        //pen = true;
        //oni = true;
        //drag = true;
        //joro = true;
    }




    void Update()
    {
        player = GameObject.FindWithTag("Player");




        // if the boss died
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
            pen = true;
            oni = false;
            drag = false;
            joro = false;
            walk = false;
        }
        else if (holder == 2)
        {
            Debug.Log("oni chosen");
            pen = false;
            oni = true;
            drag = false;
            joro = false;
            walk = false;
        }
        else if (holder == 3)
        {
            Debug.Log("dragon chosen");
            pen = false;
            oni = false;
            drag = true;
            joro = false;
            walk = false;
        }
        else if (holder == 4)
        {
            Debug.Log("Jorogumo chosen");
            pen = false;
            oni = false;
            drag = false;
            joro = true;
            walk = false;

        }
        holder = 0;


        //abilities
        if(pen == true)
        {
            penForm();
        }
        if (oni == true)
        {
            oniForm();
        }
        if (drag == true)
        {
            dragForm();

        }
        if (joro == true)
        {
            joroForm();
        }
        if(walk == true)
        {
            WalksAround();
        }
    }









    //Penanggalan - he heals a little then spews blood everywhere that the player has to dodge 
    void penForm()
    {
        if(onlyOnce == false)
        {
            Debug.Log("not made yet");
            onlyOnce = true;
            ResetBoss();
        }
    }








    //oni - he slams down onto the ground then runs at the player creating shockwaves behind him
    void oniForm()
    {
        if (onlyOnce == false)
        {
            Debug.Log("not made yet");
            onlyOnce = true;
            ResetBoss();
        }
    }








    //Dragon - the dragon form shoots a fireball then transforms into ninja form and dashes at the player 
    void dragForm()
    {
        if (onlyOnce == false)
        {
            Debug.Log("not made yet");
            onlyOnce = true;
            ResetBoss();
        }
    }







    //Jorogumo - lays some spider childern and shoots 3 webs that the player cant go through unless they shoot something at it to break it 
    void joroForm()
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


        pen = false;
        oni = false;
        drag = false;
        joro = false;
        CancelInvoke();
    }






    //heath related
    void BossDies()
    {
        //play animation - he melts into a shadow puddle and out comes a shadow version of whatever the next boss is


        //next stage
        GetComponent<b5S2>().enabled = true;
        GetComponent<b5S1>().enabled = false;

    }
}
