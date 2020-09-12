using System.Collections.Generic;
using UnityEngine;

public class PillarSpawner : MonoBehaviour
{
    SceneManager game;
    static List<GameObject> pool = new List<GameObject>();
    public float timeGap = 1;
    private float timer = 0;
    public GameObject pillar;
    public float maxY;

    void Start() {
        game = SceneManager.instance;
    }

    void Update() {
        if (game.IsRunning && timer > timeGap) {
            GameObject newPillar = Instantiate(pillar);
            newPillar.transform.position = transform.position + new Vector3(0, Random.Range(-maxY, maxY), 0);
            pool.Add(newPillar);
            Destroy(newPillar, 30);
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    void OnEnable() {
        SceneManager.OnGameOver += DeleteExistingPillars;
    }

    void OnDisable() {
         SceneManager.OnGameOver -= DeleteExistingPillars;
    }

    void DeleteExistingPillars() {
        foreach (GameObject pillar in pool) {
            DestroyImmediate(pillar);
        }
        pool.Clear();
    }

}