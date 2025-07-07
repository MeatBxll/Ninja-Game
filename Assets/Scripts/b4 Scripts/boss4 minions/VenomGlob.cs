using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomGlob : MonoBehaviour
{
    public GameObject venomPool;
    private Rigidbody2D rb;
    private GameObject boss4;
    public float horizontalForce;
    public float upForce;

    void Start()
    {
        boss4 = GameObject.FindWithTag("boss4");
        rb = GetComponent<Rigidbody2D>();
        if (transform.position.x < boss4.transform.position.x)
        {
            rb.velocity = new Vector2(-horizontalForce, upForce);
        }
        else
        {
            rb.velocity = new Vector2(horizontalForce, upForce);
        }
        
    }

    void Update()
    {


        Destroy(gameObject, 6);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {

            Instantiate(venomPool, transform.position, transform.rotation);


            Destroy(gameObject);
        }
    }
}
