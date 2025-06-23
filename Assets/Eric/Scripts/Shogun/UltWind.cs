using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltWind : MonoBehaviour
{

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

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit Enemy");
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
