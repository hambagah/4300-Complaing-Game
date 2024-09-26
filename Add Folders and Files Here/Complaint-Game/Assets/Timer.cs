using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    public TMP_Text timerText;
    bool finished = false;
    string seconds;
    private float t;
    public GameOver GameOverScreen;

    void Start () {
        t = 0f;
        GameOverScreen.Off();
    }
   
    // Update is called once per frame
    void Update () {
        t += Time.deltaTime;
        if (!finished)
        {
            seconds = (t % 60).ToString("f2");

            timerText.text = seconds;
            timerText.color = new Color(t/60f, (60f-t)/60f, 0f, 1f);
        }
        if (t >= 60f)
        {
            GameOver();
            finished = true;
        }
    }

    public void GameOver() {
        GameOverScreen.Setup(1);
    }
}