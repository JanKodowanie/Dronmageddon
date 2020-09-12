using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public float volumeScale;
    static AudioClip crashSound, tapSound, scoreSound;
    static AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volumeScale;
        crashSound = Resources.Load<AudioClip>("crash");
        tapSound = Resources.Load<AudioClip>("tap");
        scoreSound = Resources.Load<AudioClip>("score");
    }

    public static void PlaySound(string name) {
        switch(name) {
            case "crash":
                audioSource.PlayOneShot(crashSound);
                break;
            case "tap":
                audioSource.PlayOneShot(tapSound);
                break;
            case "score":
                audioSource.PlayOneShot(scoreSound);
                break;
        }
    }
}
