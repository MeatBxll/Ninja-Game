using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameThrower : MonoBehaviour
{
    public GameObject flames;
    public float fireRate;
    private float nextFire;
    public float roamSpeed;

    private bool movingRight = false;
    private float groundDetectDistance = 4;

    private float flamePos;

    void Update()
    {

        if (flames == null)
        {
            Destroy(gameObject);
           
        }
        else
        {
            if (Time.time > nextFire)
            {
                flames.SetActive(true);
                nextFire = Time.time + fireRate;
                Invoke("stopRender", (fireRate / 2) - 1);
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(-roamSpeed, GetComponent<Rigidbody2D>().velocity.y);
            RaycastHit2D groundInfo = Physics2D.Raycast(new Vector2(transform.position.x - groundDetectDistance, transform.position.y - transform.localScale.y + 1), Vector2.down, 2);
            if (groundInfo.collider == false)
            {
                turnAround();
            }

            RaycastHit2D sideInfo = Physics2D.Raycast(new Vector2(transform.position.x - groundDetectDistance, transform.position.y + transform.localScale.y / 2), Vector2.down, transform.localScale.y - .4f);
            if (sideInfo.collider != false)
            {
                if (sideInfo.collider.gameObject.tag != "Player")
                {
                    if (sideInfo.collider.gameObject.layer != 8)
                    {
                        turnAround();
                    }
                }
            }
        }
    }
    void turnAround()
    {
        roamSpeed = roamSpeed * -1;
        movingRight = !movingRight;
        groundDetectDistance = groundDetectDistance * -1;

        flamePos = Mathf.Abs(transform.position.x - flames.transform.position.x);

        if (movingRight)
        {
            flames.transform.rotation = Quaternion.Euler(flames.transform.rotation.x, 0, 12);
            flames.transform.position = new Vector2(transform.position.x + flamePos, flames.transform.position.y);
        }
        else
        {
            flames.transform.rotation = Quaternion.Euler(flames.transform.rotation.x, 180, 12);
            flames.transform.position = new Vector2(transform.position.x - flamePos, flames.transform.position.y);
        }
    }

    void stopRender()
    {
        flames.SetActive(false);
        CancelInvoke();
    }
}
