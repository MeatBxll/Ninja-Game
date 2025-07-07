using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpingRoamer : MonoBehaviour
{
    public float roamSpeed;
    public float jumpHeight;
    public float jumpRate;

    private float nextJump;
    private float groundDetectDistance = 2;
    void Start()
    {
        nextJump = Time.time;

    }


    void Update()
    {


        if (Time.time > nextJump)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            nextJump = Time.time + jumpRate;
        }


        GetComponent<Rigidbody2D>().velocity = new Vector2(-roamSpeed, GetComponent<Rigidbody2D>().velocity.y);
        RaycastHit2D groundInfo = Physics2D.Raycast(new Vector2(transform.position.x - groundDetectDistance, transform.position.y - transform.localScale.y / 2 + .1f), Vector2.down, 10);
        RaycastHit2D sideInfo = Physics2D.Raycast(new Vector2(transform.position.x - groundDetectDistance, transform.position.y + transform.localScale.y / 2), Vector2.down, transform.localScale.y - .4f);
        if (groundInfo.collider == false)
        {
            roamSpeed = roamSpeed * -1;
            groundDetectDistance = groundDetectDistance * -1;
        }
        if (sideInfo.collider != false)
        {
            if (sideInfo.collider.gameObject.layer != 8)
            {
                if (sideInfo.collider.gameObject.tag != "Player")
                {
                    roamSpeed = roamSpeed * -1;
                    groundDetectDistance = groundDetectDistance * -1;
                }
            }
        }
    }
}
