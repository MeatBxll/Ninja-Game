using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMover : MonoBehaviour
{
    
    public bool boss;
    public float cameraCurrentSpeed;
    public float increaseAmount;
    public float distBetweenIncreases;

    private Rigidbody2D rb;

    private GameObject gameController;
    

    private float playerCurrentDist;
    private float playerPrevDist;

    void Start()
    {
        gameController = GameObject.Find("gameController");
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if(playerCurrentDist > playerPrevDist)
        {
            playerPrevDist = playerCurrentDist + distBetweenIncreases;
            cameraCurrentSpeed = cameraCurrentSpeed + increaseAmount;
        }

        playerCurrentDist = gameController.GetComponent<gameController>().currentDistance;


        if (boss == false)
        {
            rb.velocity = new Vector2(cameraCurrentSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }

    }
}
