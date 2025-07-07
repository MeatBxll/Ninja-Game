using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingPlatformsScript : MonoBehaviour
{
    public List<GameObject> fallingPlatformsEvent;
    public float eventDistance;

    private float eventDistanceHolder;
    private GameObject gameController;

    private bool bossAlive;

    void Start()
    {
        gameController = GameObject.Find("gameController");
        gameController.GetComponent<endlessMap>().currentPlatformsHolder = fallingPlatformsEvent;
        eventDistanceHolder = eventDistance + gameController.GetComponent<gameController>().currentDistance;
        gameController.GetComponent<gameController>().bossAlive = bossAlive;
    }

    void Update()
    {
        if(gameController.GetComponent<gameController>().currentDistance > eventDistanceHolder)
        {
            if (!gameController.GetComponent<gameController>().bossAlive)
            {
                gameController.GetComponent<endlessMap>().eventOver = true;
                Destroy(gameObject);
            }
        }

        if (bossAlive)
        {
            if (!gameController.GetComponent<gameController>().bossAlive)
            {
                gameController.GetComponent<endlessMap>().currentPlatformsHolder = fallingPlatformsEvent;
                bossAlive = false;
            }
        }
    }
}
