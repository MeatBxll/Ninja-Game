using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b5S2 : MonoBehaviour
{
    //general stuff
    private GameObject player;
    private Rigidbody2D rb;


    void Start()
    {
        Debug.Log("stage 2 begun");
        GetComponent<Health>().GainHealth(-1);


        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        player = GameObject.FindWithTag("Player");




        //take damage and die
        if (GetComponent<Health>().currentHealth <= 0)
        {
            BossDies();
        }


    }



    void BossDies()
    {
        //play animation - the trasition from the last stage to this one is where she cacoons up and a bunch of tiny spiders crawl out while she is then she hatches out and all the baby spiders run offscreen


        //next stage
        GetComponent<b5S3>().enabled = true;
        GetComponent<b5S2>().enabled = false;

    }
}
