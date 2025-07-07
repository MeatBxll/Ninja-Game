using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaUlt : MonoBehaviour
{

    private Dash dashUlt;       //Used to call the dash function
    private int dashStore;      // Stores the original number of dashes
    public int ultNumberOfDashes;

    private NinjaHand shootUlt;         
    private float shootSpeedStore;
    public float ultShootSpeed;

    private Walking walkingUlt;
    private float walkingSpeedStore;
    public float UltWalkingSpeed;

    private float ultTime;          //Timer for the ultimate
    public float startUltTimeUp;

    private int currentUltCharge;      //Used for status of ult charge
    public int completeUltCharge;
    private bool ultAvailable;
    


    void Start()
    {

        dashUlt = GetComponent<Dash>();        
        dashStore = dashUlt.numberOfDashes;

        shootUlt = GameObject.Find("Hand").GetComponent<NinjaHand>();
        shootSpeedStore = shootUlt.startTimeBtwShots;

        walkingUlt = GetComponent<Walking>();
        walkingSpeedStore = walkingUlt.movementSpeed;

    }

    // Update is called once per frame
    void Update()
    {
       
        if (ultTime <= 0 && ultAvailable == true)       //Timer for ult
        {
            ultAvailable = false;
            
            dashUlt.numberOfDashes = dashStore;

            shootUlt.startTimeBtwShots = shootSpeedStore;
            shootUlt.ultRunning = false;

            walkingUlt.movementSpeed = walkingSpeedStore;
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

                dashUlt.numberOfDashes = ultNumberOfDashes;     // Changes the amount of dashes
                dashUlt.currentDash = ultNumberOfDashes;

                shootUlt.startTimeBtwShots = ultShootSpeed;
                shootUlt.ultRunning = true;

                walkingUlt.movementSpeed = UltWalkingSpeed;

                
                
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
