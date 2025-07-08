using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    private int hearts;
    public int maxHearts;

    private float currentInvinsibililty;
    public float maxInvinsibility;



    public GameObject[] heart;


    void Start()
    {
        hearts = maxHearts;

        heart[0] = GameObject.Find("heart0");
        heart[1] = GameObject.Find("heart1");
        heart[2] = GameObject.Find("heart2");
        heart[3] = GameObject.Find("heart3");
        heart[4] = GameObject.Find("heart4");

        for (int i = 4; i >= maxHearts; i--)
        {
            heart[i].SetActive(false);
        }
        
    }

    void Update()
    {
    currentInvinsibililty -= Time.deltaTime;
    }


    public void PlayerTookDamage()
    {
        hearts--;
        currentInvinsibililty = maxInvinsibility;

        heart[hearts].SetActive(false);

        if (hearts <= 0)
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentInvinsibililty <= 0)
        {
            if (collision.gameObject.layer == 8 || collision.gameObject.layer == 6 || collision.gameObject.layer == 9 || collision.gameObject.layer == 3)
            {
                PlayerTookDamage();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (currentInvinsibililty <= 0)
        {
            if (collision.gameObject.layer == 8 || collision.gameObject.layer == 6 || collision.gameObject.layer == 9 || collision.gameObject.layer == 3)
            {
                PlayerTookDamage();
            }
        }
    

    }


}
