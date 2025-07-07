using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShogunHand : MonoBehaviour
{   // Put on Weapon
    // Create a prefab for the projectile and drop it in the projectile slot
    // Create an empty called ShotPoint and parent it to weapon and drag the empty in the shotPint slot. (Put at the tip of the weapon)



    public float offset; // adjust to help weapon face mouse

    public GameObject projectile;   // creates the projectiles
    public Transform shotPoint;

    public bool windPunch;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;          //Used for weapon to face curser
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (windPunch == true)
        {
            Instantiate(projectile, shotPoint.position, transform.rotation);
            windPunch = false;
        }
    }
}
