using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletv2 : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [NonSerialized] public GameObject player;
    Rigidbody2D rb;

    void Start()
    {
        Invoke("DestroyBullet", lifeTime);
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player || collision.gameObject.GetComponent<Bulletv2>() != null)
            return;

        if (collision.gameObject.tag == "Enemy")    //Can change tag or anything in this function
        {
            Destroy(collision.gameObject);
            DestroyBullet();
        }

        if (collision.gameObject.tag == "Ground")
        {
            DestroyBullet();
        }


    }

    void DestroyBullet()        // Destroyes bullet after certain amount of time
    {
        Destroy(gameObject);
    }
}
