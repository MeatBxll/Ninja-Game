using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class distanceTraveled : MonoBehaviour
{
    public string dist = "12";
    public Text deathText;

    public string textValue;
    
    void Update()
    {
        textValue = "you got " + dist + "  meters from the start";

        deathText.text = textValue; 
    }
}
