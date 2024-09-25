using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private int score;
    
    private TMP_Text scoreText;

    void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        ShowScore();
    }

    public void IncreaseScore(int val)
    {
        score += val;
    }

    private void ShowScore() { scoreText.text = $"Score = {score.ToString()}"; }
}