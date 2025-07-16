using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b1SpawnerEnemy3 : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;

    private float nextJump;

    public float jumpHeight;
    public float timeBetweenJumps;
    public float moveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        player = GameObject.FindWithTag("Player");

        if (Time.time > nextJump)
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            nextJump = Time.time + timeBetweenJumps;

        }

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
