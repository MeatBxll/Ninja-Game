using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PierceArrow : MonoBehaviour
{

    public float speed;             // How fast the bullet moves
    public float lifeTime;          // How long the bullet lasts   

    Rigidbody2D rb;

    public int damage;
    private int baseEnemyDamage;

    void Start()
    {

        Invoke("DestroyBullet", lifeTime);

        rb = GetComponent<Rigidbody2D>();

        baseEnemyDamage = damage / 4;
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = transform.right * speed;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.layer == 6)   //Regualar Enemy
        {
            GameObject.Find("Bow").GetComponent<ArcherUlt>().UltimateCharge(baseEnemyDamage);
        }


        if (collision.gameObject.layer == 9)        //Boss
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            GameObject.Find("Bow").GetComponent<ArcherUlt>().UltimateCharge(damage);
        }

    }


    void DestroyBullet()        // Destroyes bullet after certain amount of time
    {
        Destroy(gameObject);
    }
}
