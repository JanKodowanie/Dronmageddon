using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HighscoreController : MonoBehaviour
{
    Text highscore;
    void OnEnable() {
        highscore = GetComponent<Text>();
        highscore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
    }
}
