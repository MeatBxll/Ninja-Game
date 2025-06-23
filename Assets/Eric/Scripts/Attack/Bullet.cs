using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour


    // Uses RayCast for detection
{


    public float speed;             // How fast the bullet moves
    public float lifeTime;          // How long the bullet lasts

    public float distance;          // How far the raycast detects
    public LayerMask whatIsSolid;   // Which layers to interact with
    
    void Start()
    {
        
        Invoke("DestroyBullet", lifeTime);      

    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);  // creates a Raycast in front of the bullet and checks if it can interact with anything
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))       // checks the tag to see if it hit an enemy
            {
                Debug.Log("Hit");
            }
            DestroyBullet();
        }


        transform.Translate(Vector2.up * speed * Time.deltaTime);

    }

    void DestroyBullet()        // Destroyes bullet after certain amount of time
    {
        Destroy(gameObject);
    }
}
