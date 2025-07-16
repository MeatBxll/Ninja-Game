using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private bool gracePeriod;
    private float timeDelay;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeDelay = Time.time + .1f;
    }


    void Update()
    {
        rb.linearVelocity = transform.right * speed;

        if(Time.time > timeDelay) { gracePeriod = true; }
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gracePeriod)
        {
            Destroy(gameObject);
        }
    }
}
