using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b1BloodSeed : MonoBehaviour
{
    public GameObject spawner;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            Instantiate(spawner, transform.position, transform.rotation);
            Destroy(gameObject, 0);
        }

        if (collision.gameObject.tag == "b1BloodPuddle")
        {
            
            Instantiate(spawner, transform.position, transform.rotation);
            Destroy(gameObject, 0);
        }

        if (collision.gameObject.tag == "Player")
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"), 0);
        }

    }
}
