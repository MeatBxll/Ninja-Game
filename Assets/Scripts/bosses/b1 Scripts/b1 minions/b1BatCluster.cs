using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b1BatCluster : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    public float flySpeed;
    public float batDurration;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");

        if (transform.position.x < player.transform.position.x)
        {
            rb.linearVelocity = new Vector2(flySpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(-flySpeed, rb.linearVelocity.y);
        }
        Destroy(gameObject, batDurration);
    }
}
