using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b4SpiderWeb1 : MonoBehaviour
{

    private GameObject spiderWeb;
    private int coolCat;
    private int gg;
    private Vector3 InitialScale;
    public GameObject startObject;
    public GameObject endObject;

    void Start()
    {
        InitialScale = transform.localScale;
        updateTransformForScale();
    }

    void Update()
    {

        if (startObject.transform.hasChanged || endObject.transform.hasChanged)
        {
            updateTransformForScale();
        }
    }
    void updateTransformForScale()
    {
        float distance =
            Vector2.Distance(a: startObject.transform.position, b: endObject.transform.position);
        transform.localScale = new Vector2(InitialScale.x, y: distance);

        Vector2 middlePoint = (startObject.transform.position + endObject.transform.position)/2;
        transform.position = middlePoint;

        Vector2 rotationDirection = (endObject.transform.position - startObject.transform.position);
        transform.up = rotationDirection;

    }
}
