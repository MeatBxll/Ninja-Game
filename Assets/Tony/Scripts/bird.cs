using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird : MonoBehaviour
{
    public float speed;
    public float switchRate;
    public float angle;

    private float switch1;
    void Update()
    {

        GetComponent<Rigidbody2D>().velocity = -transform.right * speed;

        if (switch1 < Time.time)
        {
            transform.eulerAngles = new Vector3(0, 0, angle);
            Invoke("switch2", switchRate / 2);
            switch1 = Time.time + switchRate;
        }
    }
    void switch2()
    {
        transform.eulerAngles = new Vector3(0, 0, -angle);
        CancelInvoke();
    }
    
}
