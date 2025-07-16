using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltArrow : MonoBehaviour
{

    public float speed;             // How fast the bullet moves
    public float lifeTime;          // How long the bullet lasts   

    Rigidbody2D rb;

    public Transform ultPos;      //used for Ulthitbox function (Hitbox)
    public float ultRange;
    public LayerMask WhatIsEnemies;

    public int damage;

    private bool madeContact;

    private float explosionTime;        //How long the aoe affect lasts
    public float startExplosionTime;

    void Start()
    {

        Invoke("DestroyBullet", lifeTime);

        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (madeContact == false)                       //Moves bullets 
        {
            rb.linearVelocity = transform.right * speed;
        }

        if (madeContact == true)
        {
            if (explosionTime <= 0)         //Timer
            {
                madeContact = false;
                DestroyBullet();
            }
            else
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(ultPos.position, ultRange, WhatIsEnemies); // Hit Box for the explosion

                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i].gameObject.layer == 6)
                    {
                    }

                    if (enemiesToDamage[i].gameObject.layer == 9)
                    {
                        enemiesToDamage[i].GetComponent<Health>().TakeDamage(damage);
                    }
                }

                explosionTime -= Time.deltaTime;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)         //Detects according to tags if the arrow made contact
    {
        if (collision.gameObject.layer == 7)
        {
            DestroyBullet();
            ExplodeOnImpact();
        }

        if (collision.gameObject.layer == 6)   //Regualar Enemy
        {
            DestroyBullet();
            ExplodeOnImpact();
        }


        if (collision.gameObject.layer == 9)        //Boss
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            DestroyBullet();
            ExplodeOnImpact();
        }


    }


    private void ExplodeOnImpact()                              //Code for when the arrow makes contact
    {
        GetComponent<SpriteRenderer>().enabled = false;
        madeContact = true;
        rb.linearVelocity = Vector2.zero;
        Debug.Log("Exploded");
        explosionTime = startExplosionTime;
    }

    private void OnDrawGizmosSelected()                 //Hitbox Visual
    {
        Gizmos.color = Color.black;

        Gizmos.DrawWireSphere(ultPos.position, ultRange);
    }

    void DestroyBullet()        // Destroyes bullet after certain amount of time
    {
        Destroy(gameObject);
    }
}
