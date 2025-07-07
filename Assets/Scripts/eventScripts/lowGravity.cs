using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lowGravity : MonoBehaviour
{
   /* public float eventDistance;
    public float gravityDecreaseAmount;

    private GameObject[] players;
    private GameObject gameController;
    private float eventDistanceHolder;

    private Dictionary<string,float> oldPlayerGravScales;



    void Start()
    {

        gameController = GameObject.Find("gameController");
        eventDistanceHolder = eventDistance + gameController.GetComponent<gameController>().currentDistance;

        players = GameObject.FindGameObjectsWithTag("Player");
        
        foreach (GameObject pl in players)
        {
            //saves gravity scale 
            oldPlayerGravScales.Add(new KeyValuePair<string, float>(pl.name, pl.GetComponent<Rigidbody2D>().gravityScale));

            oldPlayerGravScales[pl.name] = pl.GetComponent<Rigidbody2D>().gravityScale;








            //sets all gravity scale 
            pl.GetComponent<Rigidbody2D>().gravityScale = oldPlayerGravScales[pl.name] / gravityDecreaseAmount;        
        }


    }

    void Update()
    {
        if (gameController.GetComponent<gameController>().bossAlive)
        {
            eventDistanceHolder = eventDistance + gameController.GetComponent<gameController>().currentDistance;

            foreach (GameObject pl in players)
            {
                pl.GetComponent<Rigidbody2D>().gravityScale = oldPlayerGravScales[pl.name];
            }

        }
        else
        {
            foreach (GameObject pl in players)
            {
                pl.GetComponent<Rigidbody2D>().gravityScale = oldPlayerGravScales[pl.name] / gravityDecreaseAmount;
            }

            if (gameController.GetComponent<gameController>().currentDistance > eventDistanceHolder)
            {
                foreach (GameObject pl in players)
                {
                    pl.GetComponent<Rigidbody2D>().gravityScale = oldPlayerGravScales[pl.name];
                }

                gameController.GetComponent<endlessMap>().eventOver = true;
                Destroy(gameObject);
            }
        }
    }/*/
}
