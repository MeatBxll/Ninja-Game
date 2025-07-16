using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteor : MonoBehaviour
{

    public GameObject meteorBoom;
    public Transform boomLocation;
    public float bulletSpeed = 15;
    Rigidbody2D rb;
    GameObject player;
    Vector2 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        moveDirection = (player.transform.position - transform.position).normalized * bulletSpeed;
        rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(meteorBoom,boomLocation.position, boomLocation.rotation);
        Destroy(gameObject,0);

    }

}
