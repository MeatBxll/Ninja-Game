using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyHealth : MonoBehaviour
{

    public int ultCharge;
    
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}


