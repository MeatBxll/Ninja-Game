using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWebThingie : MonoBehaviour
{
    void Start()
    {
        transform.DetachChildren();
    }

    void Update()
    {
        Destroy(gameObject, 0);
    }
}
