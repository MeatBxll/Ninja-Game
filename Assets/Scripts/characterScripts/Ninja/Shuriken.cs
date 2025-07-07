using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{

    public float speed;             // How fast the bullet moves
    public float lifeTime;          // How long the bullet lasts   

    Rigidbody2D rb;

    public int damage;

    private int baseEnemyUltCharge;

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

        if (collision.gameObject.layer == 7)
        {
            DestroyBullet();
        }

        if (collision.gameObject.layer == 6)   //Regualar Enemy destroys enemy and gives the ninja ult charge
        {
            collision.GetComponent<BaseEnemyHealth>().DestroyEnemy();
            baseEnemyUltCharge = collision.GetComponent<BaseEnemyHealth>().ultCharge;
            DestroyBullet();
            if (CompareTag("Shuriken"))
            {   
                // checks if ninja player exists if true give him ult charge
                if (GameObject.Find("Ninja") == true) {
                    GameObject.Find("Ninja").GetComponent<NinjaUlt>().UltimateCharge(baseEnemyUltCharge);
                }
            }
        }

        
        if (collision.gameObject.layer == 9)        //Damages boss and gets ultcharge
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            DestroyBullet();
            if (CompareTag("Shuriken"))
            {   
                if (GameObject.Find("Ninja") == true) {
                    GameObject.Find("Ninja").GetComponent<NinjaUlt>().UltimateCharge(damage);
                }
            }
        }    
            

    }

    void DestroyBullet()        // Destroyes bullet after certain amount of time
    {
        Destroy(gameObject);
    }
}
