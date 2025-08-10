using System;
using UnityEngine;

public class LevelPlate : MonoBehaviour
{
    public int sceneIndex;
    [NonSerialized] public SceneLoader SceneLoader;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !SceneLoader.SceneUi.activeInHierarchy)
        {
            SceneLoader.HoveringLevel(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && SceneLoader.SceneUi.activeInHierarchy)
        {
            SceneLoader.StopHoveringLevel();
        }
    }

}
