using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherUlt : MonoBehaviour
{
    public int completeUltCharge;       //ult charge Total in damage
    public int currentUltCharge;        // Make this private later (testing)

    private bool ultChargingUp;

    private float bowChargeTime;     //Bow charge/ Pullback time
    public float startBowChargeTime;

    public GameObject projectile;   //Used for where the shot is coming from
    public Transform shotPoint;

    private int currentUltShot;     // keeps track of how many shots are left
    public int numberOfUltShot;

    public float ultMovementSpeed;
    private float movementSpeedStore;

    private float ultArrowTimer;        //How long you can have the ult out before it gets put away
    public float startUltArrowTimer;

    private bool ultArrowTimerOn;
    void Start()
    {

        movementSpeedStore = GameObject.Find("Archer").GetComponent<playerMovement>().movementSpeed;

        ultArrowTimer = startUltArrowTimer;
    }

    void Update()
    {

        if (ultArrowTimer <= 0 && ultArrowTimerOn == true)      //Ends the Ult if the timer runs out (Timer)              
        {
            currentUltShot = 0;
            GetComponent<Bow>().ultInUse = false;

            Debug.Log("Ult Ended No Time");

            GameObject.Find("Archer").GetComponent<playerMovement>().movementSpeed = movementSpeedStore;

            ultArrowTimerOn = false;
            ultArrowTimer = startUltArrowTimer;
        }
        else if (ultArrowTimerOn == true)                   //Timer
        {

            if (ultChargingUp == false)                    //Runs down the time if the ult is not currently being charged back 
            {
                ultArrowTimer -= Time.deltaTime;
            }

            if (currentUltShot > 0)                         // Checks if there is any shots available
            {
                if (Input.GetMouseButtonDown(0) && ultChargingUp == false)
                {
                    ultChargingUp = true;                                               //Starts ult shot chargeUp Timer and sets proper time and disables necessary scripts
                    bowChargeTime = startBowChargeTime;
                    currentUltShot--;
                    Debug.Log("Ult Used");

                    GameObject.Find("Archer").GetComponent<playerMovement>().enabled = false;
                }
            }

            if (bowChargeTime <= 0 && ultChargingUp == true)                            // Timer for charging arrow
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);        //Shoots arrow, enables scripts, and resets timer back to proper time
                ultChargingUp = false;
                ultArrowTimer = startUltArrowTimer;

                GameObject.Find("Archer").GetComponent<playerMovement>().enabled = true;

                if (currentUltShot <= 0)
                {
                    GameObject.Find("Archer").GetComponent<playerMovement>().movementSpeed = movementSpeedStore;                 //Resets movement and variables if there is no more shots available
                    GetComponent<Bow>().ultInUse = false;
                    ultArrowTimerOn = false;

                }
            }
            else if (ultChargingUp == true)
            {
                bowChargeTime -= Time.deltaTime;                    //Timer
            }

            if (Input.GetKeyDown(KeyCode.Q) && currentUltShot > 0 && ultChargingUp == false)        //Resets all variables and scripts if the ult button is pressed again while the ult is running
            {
                currentUltShot = 0;
                GetComponent<Bow>().ultInUse = false;
                ultArrowTimer = startUltArrowTimer;
                ultArrowTimerOn = false;

                GetComponent<playerMovement>().movementSpeed = movementSpeedStore;
            }
        }

        if (currentUltCharge >= completeUltCharge)
        {
            if (Input.GetKey(KeyCode.Q))                //checks if the requirements are met for ult
            {
                currentUltShot = numberOfUltShot;

                currentUltCharge = 0;

                GetComponent<Bow>().ultInUse = true;

                GameObject.Find("Archer").GetComponent<playerMovement>().movementSpeed = ultMovementSpeed;

                ultArrowTimerOn = true;
            }


        }
    }

    public void UltimateCharge(int damage)                                      //keeps track of ult charge
    {
        if (currentUltCharge < completeUltCharge && currentUltShot <= 0)
        {
            currentUltCharge += damage;
            Debug.Log(currentUltCharge);
        }

    }
}
