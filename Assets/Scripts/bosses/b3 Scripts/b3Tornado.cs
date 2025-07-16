using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b3Tornado : MonoBehaviour
{
    public float speed = 15;
    Rigidbody2D rb;
    GameObject player;
    Vector2 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        moveDirection = (player.transform.position - transform.position).normalized * speed;
        rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y);
    }
}
