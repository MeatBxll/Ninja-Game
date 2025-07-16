using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sboss4Ammo : MonoBehaviour
{

    public float bulletSpeed = 15;
    private Rigidbody2D rb;
    GameObject player;
    Vector2 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        moveDirection = (player.transform.position - transform.position).normalized * bulletSpeed;
        rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3);
    }

    void Update()
    {

    }
}
