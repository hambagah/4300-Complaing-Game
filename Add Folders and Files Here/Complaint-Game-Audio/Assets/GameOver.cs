using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void Setup(int score) {
        gameObject.SetActive(true);

    }

    public void Off() {
        gameObject.SetActive(false);
    }
}
