using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangedUnitAmmo : MonoBehaviour
{

    public float speed;
    GameObject player;
    Vector2 moveDirection;

    //closestPlayer Variables 
    private GameObject[] players;
    private float close;

    private void Start()
    {
        //checking closest player
        players = GameObject.FindGameObjectsWithTag("Player");


        close = Mathf.Infinity;
        foreach (GameObject pl in players)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, pl.transform.position);
            if (distanceToPlayer < close)
            {
                close = distanceToPlayer;
                player = pl;
            }
        }

        if (player == null)
        {
            Destroy(gameObject);
        }

        moveDirection = (player.transform.position - transform.position).normalized * speed;
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }


}
