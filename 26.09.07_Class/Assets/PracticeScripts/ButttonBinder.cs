using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButttonBinder : MonoBehaviour
{
    [SerializeField] private Button _codeButton;
    [SerializeField] private ScoreBoard _scoreBoard;

    private void Start()
    {
        BindButtonEvents();
    }

    private void BindButtonEvents()
    {
        _codeButton.onClick.AddListener(_scoreBoard.AddScore);
    }
}
