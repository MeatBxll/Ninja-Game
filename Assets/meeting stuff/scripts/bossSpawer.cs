using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSpawer : MonoBehaviour
{
    public GameObject currentBoss;
    private GameObject mainCamera;
    private GameObject gameController;
    private bool activateBoss;
    private bool onlyOnce;

    void Update()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        gameController = GameObject.Find("gameController");

        if (currentBoss == null)
        {
            gameController.GetComponent<gameController>().healthBar.SetActive(false);
            mainCamera.GetComponent<cameraMover>().boss = false;
            gameController.GetComponent<gameController>().continueMap = true;
            Destroy(gameObject);
        }




        if (activateBoss == true)
        {
            gameController.GetComponent<gameController>().healthBar.SetActive(true);
            currentBoss.SetActive(true);
            activateBoss = false;
        }
        if (onlyOnce == false)
        {
            if (mainCamera.transform.position.x >= currentBoss.transform.position.x - 1 && mainCamera.transform.position.x <= currentBoss.transform.position.x)
            {
                mainCamera.GetComponent<cameraMover>().boss = true;


                activateBoss = true;
                onlyOnce = true;

            }
        }
    }
}
