using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b1BloodSpit2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private GameObject player;

    public GameObject bloodPuddle;
    public float bloodSpitSpeed;
    public float distanceFromPlayer;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        moveDirection = (player.transform.position - transform.position).normalized * bloodSpitSpeed;
        rb.linearVelocity = new Vector2(moveDirection.x + distanceFromPlayer, moveDirection.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            Instantiate(bloodPuddle, transform.transform.position, transform.transform.rotation);
            Destroy(gameObject, 0);
        }
        if (collision.gameObject.tag != "ground")
        {
            Destroy(gameObject, 0);
        }
    }
}
