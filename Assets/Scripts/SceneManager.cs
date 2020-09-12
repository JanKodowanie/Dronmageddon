﻿using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOver;

    public static SceneManager instance;
    public Text currentScore;
    public Text scoreOnEnd;
    public GameObject startScene;
    public GameObject gameOverScene;
    public GameObject countdownScene;

    int score = 0;
    bool isRunning = false;

    void Start() {
        currentScore.gameObject.SetActive(false);
        SetCurrentScene(SceneType.Start);
    }

    void OnEnable() {
        CountdownController.OnCountdownEnd += OnCountdownEnd;
        DroneController.OnGapPassed += OnGapPassed;
        DroneController.OnDroneCrashed += OnDroneCrashed;
    }

    void OnDisable() {
        CountdownController.OnCountdownEnd -= OnCountdownEnd;
        DroneController.OnGapPassed -= OnGapPassed;
        DroneController.OnDroneCrashed -= OnDroneCrashed;
    }

    public void StartGame() {
        OnGameStarted();
        currentScore.gameObject.SetActive(false);
        SetCurrentScene(SceneType.Countdown);
    }

    public void RestartGame() {
        OnGameOver();
        currentScore.text = "0";
        SetCurrentScene(SceneType.Start);
    }

    void Awake() {
        instance = this;
    }

    void OnCountdownEnd() {
        SetCurrentScene(SceneType.None);
        currentScore.gameObject.SetActive(true);
        OnGameStarted();
        score = 0;
        isRunning = true;
    }

    void OnGapPassed() {
        score++;
        currentScore.text = score.ToString();
    }

    void OnDroneCrashed() {
        isRunning = false;
        currentScore.gameObject.SetActive(false);

        int currentHighscore = PlayerPrefs.GetInt("Highscore");
        if (score > currentHighscore) {
            PlayerPrefs.SetInt("Highscore", score);
        }
        SetCurrentScene(SceneType.GameOver);
        scoreOnEnd.text = "Score: " + score.ToString();
    }

    void SetCurrentScene(SceneType scene) {
        switch (scene) {
            case SceneType.None:
                startScene.SetActive(false);
                gameOverScene.SetActive(false);
                countdownScene.SetActive(false);
                break;
            case SceneType.Start:
                startScene.SetActive(true);
                gameOverScene.SetActive(false);
                countdownScene.SetActive(false);
                break;
            case SceneType.Countdown:
                startScene.SetActive(false);
                gameOverScene.SetActive(false);
                countdownScene.SetActive(true);
                break;
            case SceneType.GameOver:
                startScene.SetActive(false);
                gameOverScene.SetActive(true);
                countdownScene.SetActive(false);
                break;
        }
    }

    public bool IsRunning {
        get {
            return isRunning;
        }
    }

    enum SceneType {
        None,
        Start,
        GameOver,
        Countdown
    }
}
