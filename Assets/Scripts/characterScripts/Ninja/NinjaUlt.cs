using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaUlt : MonoBehaviour
{

    private int dashStore;      // Stores the original number of dashes
    public int ultNumberOfDashes;

    private NinjaHand shootUlt;
    private float shootSpeedStore;
    public float ultShootSpeed;

    private float walkingSpeedStore;
    public float UltWalkingSpeed;

    private float ultTime;          //Timer for the ultimate
    public float startUltTimeUp;

    private int currentUltCharge;      //Used for status of ult charge
    public int completeUltCharge;
    private bool ultAvailable;



    void Start()
    {


        shootUlt = GameObject.Find("Hand").GetComponent<NinjaHand>();
        shootSpeedStore = shootUlt.startTimeBtwShots;

        walkingSpeedStore = GetComponent<playerMovement>().movementSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        if (ultTime <= 0 && ultAvailable == true)       //Timer for ult
        {
            ultAvailable = false;


            shootUlt.startTimeBtwShots = shootSpeedStore;
            shootUlt.ultRunning = false;

            GetComponent<playerMovement>().movementSpeed = walkingSpeedStore;
        }
        else if (ultAvailable == true)
        {
            ultTime -= Time.deltaTime;
        }




        if (currentUltCharge >= completeUltCharge)      // Ult activation
        {
            if (Input.GetKey(KeyCode.Q))
            {
                ultTime = startUltTimeUp;
                currentUltCharge = 0;
                ultAvailable = true;


                shootUlt.startTimeBtwShots = ultShootSpeed;
                shootUlt.ultRunning = true;

                GetComponent<playerMovement>().movementSpeed = UltWalkingSpeed;



            }
        }
    }

    public void UltimateCharge(int damage)                                      //keeps track of ult charge
    {

        if (currentUltCharge < completeUltCharge && ultAvailable == false)
        {
            currentUltCharge += damage;
        }

    }
}
