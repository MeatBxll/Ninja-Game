using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBoss5Boom : MonoBehaviour
{
    public float boomDurration;
    void Start()
    {
        Destroy(gameObject, boomDurration);
    }
}
