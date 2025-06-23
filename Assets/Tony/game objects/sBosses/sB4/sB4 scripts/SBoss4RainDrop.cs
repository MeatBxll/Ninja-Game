using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBoss4RainDrop : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 0);
    }
}
