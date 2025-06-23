using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b4WebStartPoint : MonoBehaviour
{
    private GameObject boss4;
    void Start()
    {
        boss4 = GameObject.FindWithTag("boss4");
        transform.parent = boss4.transform;
    }
}
