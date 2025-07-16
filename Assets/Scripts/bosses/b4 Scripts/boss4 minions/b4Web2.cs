using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b4Web2 : MonoBehaviour
{
    private GameObject boss4;
    private Rigidbody2D rb;
    private bool stop;
    private bool b4Right;
    private float randomHolder;
    private float stopTimeHolder;

    public float stopTime;
    public float speed1;
    public float height1;
    public float speed2;
    public float height2;
    public float speed3;
    public float height3;
    public float speed4;
    public float height4;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boss4 = GameObject.FindWithTag("boss4");

        if (transform.position.x < boss4.transform.position.x)
        {
            b4Right = true;
        }

        randomHolder = Random.Range(1, 5);



        stopTimeHolder = Time.time + stopTime;


    }

    void Update()
    {
        if(stop == false)
        {
            if(b4Right == true)
            {
                if(randomHolder == 1)
                {
                    rb.linearVelocity = new Vector2(-speed1,height1);
                }
                if (randomHolder == 2)
                {
                    rb.linearVelocity = new Vector2(-speed2, height2);
                }
                if (randomHolder == 3)
                {
                    rb.linearVelocity = new Vector2(-speed3,height3);
                }
                if (randomHolder == 4)
                {
                    rb.linearVelocity = new Vector2(-speed4, height4);
                }
            }
            else
            {
                if (randomHolder == 1)
                {
                    rb.linearVelocity = new Vector2(speed1, height1);
                }
                if (randomHolder == 2)
                {
                    rb.linearVelocity = new Vector2(speed2, height2);
                }
                if (randomHolder == 3)
                {
                    rb.linearVelocity = new Vector2(speed3, height3);
                }
                if (randomHolder == 4)
                {
                    rb.linearVelocity = new Vector2(speed4, height4);
                }
            }
        }
        if (stopTimeHolder != 0)
        {
            if (stopTimeHolder < Time.time)
            {
                stop = true;
                rb.linearVelocity = new Vector2(0, 0);
                stopTimeHolder = 0;
            }
        }


    }
}
