using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roamer : MonoBehaviour
{
    public float roamSpeed;

    private float groundDetectDistance = 2;

    void Update()
    {
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-roamSpeed, GetComponent<Rigidbody2D>().linearVelocity.y);
        RaycastHit2D groundInfo = Physics2D.Raycast(new Vector2(transform.position.x - groundDetectDistance, transform.position.y - transform.localScale.y/2 + .1f), Vector2.down, 2);
        RaycastHit2D sideInfo = Physics2D.Raycast(new Vector2(transform.position.x - groundDetectDistance, transform.position.y + transform.localScale.y / 2), Vector2.down, transform.localScale.y - .4f);
        if (groundInfo.collider == false)
        {
            roamSpeed = roamSpeed * -1;
            groundDetectDistance = groundDetectDistance * -1; 
        }
        if(sideInfo.collider != false)
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
