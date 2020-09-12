using UnityEngine;

public class PillarController : MonoBehaviour
{
    public float speed;
    SceneManager game;

    void Start()
    {
        game = SceneManager.instance;
    }

    void Update()
    {
        if (game.IsRunning) {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
}