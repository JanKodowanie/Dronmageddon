using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DroneController : MonoBehaviour
{
    public delegate void DroneDelegate();
    public static event DroneDelegate OnDroneCrashed;
    public static event DroneDelegate OnGapPassed;

    public float tapForce = 10;
    public float tiltForce = 5;
    SceneManager game;
    public Vector3 startPos;
    Rigidbody2D rigidBody;
    Quaternion downRotation;
    Quaternion forwardRotation;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0, 0, -30);
        forwardRotation = Quaternion.Euler(0, 0, 30);
        game = SceneManager.instance;
        transform.localPosition = startPos;
        transform.rotation = Quaternion.identity;
        rigidBody.simulated = false;
    }

    void Update() {
        if (!game.IsRunning) return;
        if (!rigidBody.simulated) return;
        if (Input.GetMouseButtonDown(0)) {
            transform.rotation = forwardRotation;
            rigidBody.velocity = Vector3.zero;
            rigidBody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
            AudioManager.PlaySound("tap");
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltForce * Time.deltaTime);
    }

    void OnEnable() {
        SceneManager.OnGameStarted += OnGameStarted;
        SceneManager.OnGameOver += OnGameOver;
        CountdownController.OnCountdownEnd += OnCountdownEnd;
    }

    void OnDisable() {
        SceneManager.OnGameStarted -= OnGameStarted;
        SceneManager.OnGameOver -= OnGameOver;
        CountdownController.OnCountdownEnd -= OnCountdownEnd;
    }

    void OnGameStarted() {
        rigidBody.velocity = Vector3.zero;
    }

    void OnCountdownEnd() {
        rigidBody.simulated = true;
    }

    void OnGameOver() {
        transform.localPosition = startPos;
        transform.rotation = Quaternion.identity;
        rigidBody.simulated = false;
    }

    void OnTriggerEnter2D(Collider2D col) {
            if (col.gameObject.tag == "Sky") {
                rigidBody.velocity = Vector3.zero;
            }
            if (col.gameObject.tag == "ScoreZone") {
                AudioManager.PlaySound("score");
                OnGapPassed();
            }
            if (col.gameObject.tag == "Obstacle") {
                rigidBody.simulated = false;
                AudioManager.PlaySound("crash");
                OnDroneCrashed();
            }
        }
}