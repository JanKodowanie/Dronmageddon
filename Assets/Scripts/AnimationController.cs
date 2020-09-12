using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    public Animator animator;
    public GameObject instance;
    public Vector3 defaultPos;

    void Start() {
        instance.transform.localPosition = defaultPos;
        animator.enabled = false;
    }

    void OnEnable() {
        CountdownController.OnCountdownEnd += EnableBackgroundAnim;
        SceneManager.OnGameOver += ResetObjectPosition;
        DroneController.OnDroneCrashed += DisableBackgroundAnim;
    }

    void OnDisable() {
        CountdownController.OnCountdownEnd -= EnableBackgroundAnim;
        SceneManager.OnGameOver -= ResetObjectPosition;
        DroneController.OnDroneCrashed -= DisableBackgroundAnim;
    }

    void DisableBackgroundAnim() {
        animator.enabled = false;
    }

    void EnableBackgroundAnim() {
        animator.enabled = true;
    }

    void ResetObjectPosition() {
        animator.gameObject.SetActive(false);
        animator.gameObject.SetActive(true);
        instance.transform.localPosition = defaultPos;
    }
}