using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b1SpawnerEnemy1 : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;

    public float moveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    
        player = GameObject.FindWithTag("Player");


        if (transform.position.x < player.transform.position.x)
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
        }

    }
}
