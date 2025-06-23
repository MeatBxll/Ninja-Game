using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBoss4RainDropSpawner : MonoBehaviour
{
    private float fireTime;
    private float random;

    public float fireRate;
    public GameObject rainDrop;
    public GameObject movingRainDrop;



    private void Start()
    {
        fireTime = Random.Range(0, 3);
    }
    void Update()
    {
        if(Time.time > fireTime)
        {
            random = Random.Range(1, 200);
            fireTime = Time.time + fireRate;
        }

        if(random != 0)
        {
            if(random != 30)
            {
                Instantiate(rainDrop, transform.position, transform.rotation);
                random = 0;
            }
            else
            {
                Instantiate(movingRainDrop, transform.position, transform.rotation);
                random = 0;

                Debug.Log("moving rain spawned");
            }
        }

    }
}
