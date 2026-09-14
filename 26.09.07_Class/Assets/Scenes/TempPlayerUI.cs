using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TempPlayerUI : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _playerHealthText;
    
    public void RefreshHealthUI(int health)
    {
        Debug.Log("UI");
        _playerHealthText.text = health.ToString();
    }
}
