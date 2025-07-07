using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingPlatforms : MonoBehaviour
{
    public float timeBeforePlatformFalls;
    private float timerHolder;
    private bool playerTouched;
    void Update()
    {
        if (playerTouched)
        {
            if(Time.time > timerHolder)
            {
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                gameObject.GetComponent<fallingPlatforms>().enabled = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerTouched = true;
            timerHolder = timeBeforePlatformFalls + Time.time;
        }
    }
}
