using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sB1Idle : MonoBehaviour
{
    public float idleTime;
    private float idleHolder;
    private bool onlyOnce;
    void Start()
    {
        
    }

    void Update()
    {
        if(onlyOnce == false)
        {
            Debug.Log("working");
            idleHolder = idleTime + Time.time;
            onlyOnce = true;
        }
        else if (idleHolder <= Time.time)
        {
            GetComponent<SBoss1>().enabled = true;
            onlyOnce = false;
            GetComponent<sB1Idle>().enabled = false;

        }
    }
}
