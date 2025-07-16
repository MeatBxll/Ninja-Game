using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltShockwave : MonoBehaviour
{

    public float speed;             // How fast the bullet moves
    public float lifeTime;          // How long the bullet lasts   

    Rigidbody2D rb;

    public int damage;

    void Start()
    {

        Invoke("DestroyBullet", lifeTime);

        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        rb.linearVelocity = transform.right * speed;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 6)   //Regualar Enemy
        {
            DestroyBullet();
        }


        if (collision.gameObject.layer == 9)        //Boss
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            DestroyBullet();
        }


    }

    void DestroyBullet()        // Destroyes bullet after certain amount of time
    {
        Destroy(gameObject);
    }
}
