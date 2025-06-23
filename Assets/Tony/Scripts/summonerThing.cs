using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class summonerThing : MonoBehaviour
{
    public float speed;
    public GameObject[] enemyAmount; 
    public GameObject summonedEnemy;

    private float holder = -1;
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        for (int i = 0; i < enemyAmount.Length; i++)
        {
            enemyAmount[i] = Instantiate(summonedEnemy, new Vector2(transform.position.x + holder, transform.position.y), transform.rotation);
            holder++;
        }
        Destroy(gameObject);
    }
}