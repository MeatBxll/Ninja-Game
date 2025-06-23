using System.Collections.Generic;
using UnityEngine;

public class endlessMap : MonoBehaviour
{
    //normal stuff
    public float spawnRange;
    public int platformDistHigh;
    public int platformDistLow;
    public List<GameObject> normalPlatforms;
    public GameObject currentPlatform;
    
    private GameObject lastPlatform;
    private GameObject player;
    private GameObject spawnedLevelPart;


    //event stuff
    public float distanceForEventsToStart;
    public List<GameObject> events;

    private float distanceForEventsToStartHolder;

    //event stuff that is changed from other script
    public bool eventOver;


    //holder for current event platforms or normal platforms
    public List<GameObject> currentPlatformsHolder;



    private void Start()
    {
        currentPlatformsHolder = normalPlatforms;

        distanceForEventsToStartHolder = distanceForEventsToStart;
    }

    void Update()
    {

        player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            GetComponent<endlessMap>().enabled = false;
        }
        else
        {
            //when event starts
            if(distanceForEventsToStartHolder != 0)
            {
                if (GetComponent<gameController>().currentDistance > distanceForEventsToStartHolder)
                {
                    Instantiate(events[Random.Range(0, events.Count)]);
                    distanceForEventsToStartHolder = 0;
                }
            }

            if (eventOver)
            {
                currentPlatformsHolder = normalPlatforms;
                distanceForEventsToStartHolder = distanceForEventsToStart + GetComponent<gameController>().currentDistance;
                eventOver = false;
            }



            //spawn new platforms
            if (player.transform.position.x > currentPlatform.transform.Find("endPos").position.x - spawnRange)
            {
                lastPlatform = currentPlatform;
                PickRandomPlatform();
            }

        }
    }

    void PickRandomPlatform()
    {
        spawnedLevelPart = currentPlatformsHolder[Random.Range(0, currentPlatformsHolder.Count)];
        currentPlatform = Instantiate(spawnedLevelPart, new Vector2(lastPlatform.transform.Find("endPos").position.x + Random.Range(platformDistLow, platformDistHigh), lastPlatform.transform.Find("endPos").position.y), lastPlatform.transform.Find("endPos").rotation);
    }

}
