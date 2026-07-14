using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    private int score = 0;

    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();

    }

    void Start()
    {
        ResetScore();
        RefreshUI();
    }

    internal void IncreaseScore(int increase)
    {
        score += increase;
        RefreshUI();
    }

    internal void RefreshUI()
    {
        scoreText.text = "Score: " + score;
    }

    internal void ResetScore()
    {
        score = 0;
    }
}
