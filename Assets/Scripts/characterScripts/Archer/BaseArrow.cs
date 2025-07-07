using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseArrow : MonoBehaviour
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
        if (collision.gameObject.layer == 7)
        {
            DestroyBullet();
        }

        if (collision.gameObject.layer == 6)   //Regualar Enemy
        {
            collision.GetComponent<BaseEnemyHealth>().DestroyEnemy();
            GameObject.Find("Bow").GetComponent<ArcherUlt>().UltimateCharge(baseEnemyDamage);
            DestroyBullet();
        }


        if (collision.gameObject.layer == 9)        //Boss
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            GameObject.Find("Bow").GetComponent<ArcherUlt>().UltimateCharge(damage);
            DestroyBullet();
        }

    }

    void DestroyBullet()        // Destroyes bullet after certain amount of time
    {
        Destroy(gameObject);
    }
}
