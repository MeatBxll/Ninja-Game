using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b1BloodPuddle : MonoBehaviour
{
    public float stayDurration;
    private GameObject player;
    void Start()
    {
        Destroy(gameObject, stayDurration);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(GameObject.FindWithTag("Player"), 0);
        }
    }
}
