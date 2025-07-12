using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b4Egg : MonoBehaviour
{
    public float hatchTime;
    public GameObject babySpiderThingie;
    void Update()
    {
        Invoke("EggHatch", hatchTime);
    }


    void EggHatch()
    {
        Instantiate(babySpiderThingie, transform.position, transform.rotation);
        Destroy(gameObject);
        CancelInvoke();
    }
}
