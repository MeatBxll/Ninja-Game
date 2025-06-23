using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public List<GameObject> enemyList; 

    private GameObject spawnedEnemy;

    void Update()
    {
        spawnedEnemy = enemyList[Random.Range(0, enemyList.Count)];
        Instantiate(spawnedEnemy, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
