using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public AudioSource hitSFX;
    public AudioSource missSFX;
    public TextMeshProUGUI scoreText;
    static int comboScore;
    void Start()
    {
        instance = this;
        comboScore = 0;
    }

    public static void Hit()
    {
        comboScore += 1;
        instance.hitSFX.Play();
    }

    public static void Miss()
    {
        comboScore = 0;
        instance.missSFX.Play();
    }

    void Update()
    {
        scoreText.text = comboScore.ToString();
    }
}
