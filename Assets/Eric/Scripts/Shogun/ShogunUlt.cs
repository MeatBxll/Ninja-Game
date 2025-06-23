using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShogunUlt : MonoBehaviour
{

    public int completeUltCharge;       //ult charge Total in damage
    public int currentUltCharge;            // public for testing (Make private later)

    private bool ultActive;

    public Transform ultPos;      //used for Ulthitbox function (Hitbox)
    public float ultRange;
    public LayerMask WhatIsEnemies;

    public GameObject projectile;
    public Transform shotPoint;

    public int damage;

    private float ultDuration;
    public float startUltDuration;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (ultActive == true)
        {
            if (ultDuration <= 0)
            {
                ultActive= false;
                Instantiate(projectile, shotPoint.position, transform.rotation);
                GameObject.Find("Shogun").GetComponent<Punch>().ultInUse = false;
            }
            else
            {
                UltHitbox();

                ultDuration -= Time.deltaTime;
            }
        }

        



        if (Input.GetKey(KeyCode.Q) && currentUltCharge >= completeUltCharge)
        {

            ultActive = true;
            ultDuration = startUltDuration;
            currentUltCharge = 0;
            GameObject.Find("Shogun").GetComponent<Punch>().ultInUse = true;

        }


    }

    private void UltHitbox()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(ultPos.position, ultRange, WhatIsEnemies);  //Checks if the enemy has collided with hitbox

        for (int i = 0; i < enemiesToDamage.Length; i++)        //loop for all enemeies in hitbox
        {
            enemiesToDamage[i].GetComponent<Health>().TakeDamage(damage);
        }

    }

    public void UltimateCharge(int damage)                                      //keeps track of ult charge
    {
        if (currentUltCharge < completeUltCharge && ultActive == false)
        {
            currentUltCharge += damage;
        }

    }

    private void OnDrawGizmosSelected()                 //Hitbox Visual
    {
        Gizmos.color = Color.black;

        Gizmos.DrawWireSphere(ultPos.position, ultRange);
    }
}
