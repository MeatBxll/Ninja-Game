using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBoss5Clone : MonoBehaviour
{
    public float cloneDownForce;
    public float cloneHorizontalForce;

    private GameObject player;
    private Rigidbody2D rb;
    void Start()
    {


        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.down * cloneDownForce, ForceMode2D.Impulse);
        player = GameObject.FindWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            if (transform.position.x < player.transform.position.x)
            {
                rb.velocity = new Vector2(cloneHorizontalForce, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-cloneHorizontalForce, rb.velocity.y);
            }
        }
    }
}
