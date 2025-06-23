using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss4S1 : MonoBehaviour
{
    //stage 1 - pretty lady she doesnt attack the player just makes noises and flies across the screen making the player feel bad for attacking her but if the player doenst attack her for 3 seconds then she leaps at him and latches on with 4 massive spider legs spins him into a little web.


    //general stuff
    private bool onlyOnce;
    private float holder;
    private float resetHolder;
    private Rigidbody2D rb;
    private GameObject player;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //stage 1 - pretty lady she doesnt attack the player just makes noises and flies across the screen making the player feel bad for attacking her but if the player doenst attack her for 3 seconds then she leaps at him and latches on with 4 massive spider legs spins him into a little web.
        







        //dies
        if (GetComponent<Health>().currentHealth <= 0)
        {
            BossDies();
        }
    }







    void BossDies()
    {
        //play animation - the trasition from the last stage to this one is where she cacoons up and a bunch of tiny spiders crawl out while she is then she hatches out and all the baby spiders run offscreen


        //next stage
        GetComponent<boss4S2>().enabled = true;
        GetComponent<boss4S1>().enabled = false;

    }
}
