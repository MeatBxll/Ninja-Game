using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaHand : MonoBehaviour
{   // Put on Weapon
    // Create a prefab for the projectile and drop it in the projectile slot
    // Create an empty called ShotPoint and parent it to weapon and drag the empty in the shotPint slot. (Put at the tip of the weapon)



    public float offset; // adjust to help weapon face mouse

    public GameObject projectile;   // creates the projectiles
    public Transform shotPoint;

    private float timeBtwShots;         // timer for the time between each shot
    public float startTimeBtwShots;




    public bool ultRunning;
    public Transform ultShotPoint1;
    public Transform ultShotPoint2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;          //Used for weapon to face curser
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);



        if (ultRunning == true)
        {
            projectile.tag = "Untagged";
        }
        else
        {
            projectile.tag = "Shuriken";
        }


        if (timeBtwShots <= 0)      // timer for time between each shot
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);    // spawns projectiles
                timeBtwShots = startTimeBtwShots;


                if (ultRunning == true)
                {
                    Instantiate(projectile, ultShotPoint1.position, transform.rotation);
                    Instantiate(projectile, ultShotPoint2.position, transform.rotation);
                }                
                
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

    }
}
