using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    
    private int _score;

    private void Start()
    {
        UpdateText();
    }

    public void AddScore()
    {
        _score += 10;
        UpdateText();
    }

    private void UpdateText()
    {
        _scoreText.text = $"Score: {_score}";
    }
}
