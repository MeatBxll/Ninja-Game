using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletv2 : MonoBehaviour
{
    //Uses OnTriggerEnter2D to detect collisions
    //Bullet needs Rigidbody2D and Collider2D depending on shape and check trigger box
    // Object its colliding with also needs collider2d


    public float speed;             // How fast the bullet moves
    public float lifeTime;          // How long the bullet lasts   

    Rigidbody2D rb;

    void Start()
    {

        Invoke("DestroyBullet", lifeTime);

        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = transform.right * speed;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy")    //Can change tag or anything in this function
        {            
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
