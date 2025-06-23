using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    //if player dies variables
    public GameObject healthBar;
    public GameObject gameOver;
    public float currentDistance;
    
    private GameObject player;
    private float startX;
    private float currentX;




    //boss event variables
    public List<float> bossEvents;
    public List<GameObject> BossPlatforms;
    public bool continueMap;

    private Transform lastPlatform;

    private float bossPlatformDistance;
    private float currentBoss;
    private int bN;
    private GameObject currentBossPlatform;


    //used in other scripts
    public bool bossAlive;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        startX = player.transform.position.x;
        bossPlatformDistance = GetComponent<endlessMap>().platformDistHigh;
        currentBoss = bossEvents[0];
    }


    void Update()
    {
        player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            gameOver.GetComponent<distanceTraveled>().dist = currentDistance.ToString();
            gameOver.SetActive(true);
            GetComponent<gameController>().enabled = false;
        }

        //everything needs to be in this else statement
        else
        {
            currentX = player.transform.position.x;
            currentDistance = currentX - startX;

            if (bN != BossPlatforms.Count)
            {
                if (currentDistance > currentBoss)
                {
                    bossAlive = true;
                    GetComponent<endlessMap>().enabled = false;
                    lastPlatform = GetComponent<endlessMap>().currentPlatform.transform.Find("endPos").transform;
                    currentBossPlatform = Instantiate(BossPlatforms[bN], new Vector2(lastPlatform.position.x + bossPlatformDistance, lastPlatform.position.y), lastPlatform.rotation);
                    bN++;
                    if (bN != BossPlatforms.Count) { currentBoss = bossEvents[bN]; }
                }
            }

            if (continueMap)
            {
                bossAlive = false;
                GetComponent<endlessMap>().enabled = true;
                GetComponent<endlessMap>().currentPlatform = currentBossPlatform;
                continueMap = false;
            }
        }
    }
}
