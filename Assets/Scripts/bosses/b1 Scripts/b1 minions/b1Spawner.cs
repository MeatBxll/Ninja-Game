using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b1Spawner : MonoBehaviour
{
    public GameObject b1Spot1;
    public GameObject b1Spot2;
    public GameObject b1Spot3;
    public GameObject b1Spot4;
    public GameObject b1Spot5;
    public GameObject b1Spot6;
    public GameObject b1Spot7;
    public GameObject b1Spot8;
    public GameObject b1Spot9;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    private float spawnHolder;
    private float randomRangeHolder;
    public float spawnRate;
    void Start()
    {
        
    }

    void Update()
    {
        if (spawnHolder < Time.time)
        {
            randomRangeHolder = Random.Range(1, 10);
            spawnHolder = Time.time + spawnRate;
        }

        if (randomRangeHolder != 0)
        {
            if(randomRangeHolder == 1)
            {
                Instantiate(enemy1, b1Spot1.transform.position, transform.rotation);
            }
            if (randomRangeHolder == 2)
            {
                Instantiate(enemy2, b1Spot2.transform.position, transform.rotation);
            }
            if (randomRangeHolder == 3)
            {
                Instantiate(enemy3, b1Spot3.transform.position, transform.rotation);
            }
            if (randomRangeHolder == 4)
            {
                Instantiate(enemy1, b1Spot4.transform.position, transform.rotation);
            }
            if (randomRangeHolder == 5)
            {
                Instantiate(enemy2, b1Spot5.transform.position, transform.rotation);
            }
            if (randomRangeHolder == 6)
            {
                Instantiate(enemy3, b1Spot6.transform.position, transform.rotation);
            }
            if (randomRangeHolder == 7)
            {
                Instantiate(enemy1, b1Spot7.transform.position, transform.rotation);
            }
            if (randomRangeHolder == 8)
            {
                Instantiate(enemy2, b1Spot8.transform.position, transform.rotation);
            }
            if (randomRangeHolder == 9)
            {
                Instantiate(enemy3, b1Spot9.transform.position, transform.rotation);
            }
            randomRangeHolder = 0;
        }






    }
}
