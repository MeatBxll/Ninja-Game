using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private gameController gameController;
    [SerializeField] private Vector3 playerSpawn;
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();
        gameController.playerSpawn = playerSpawn;
        gameController.SceneLoaded(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        gameController.LevelCompleted();
    }
}
