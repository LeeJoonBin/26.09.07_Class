using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EnemyUIController : MonoBehaviour
{
    [SerializeField] private Transform _guageTransform;
    [SerializeField] private Image _healthBar;
    [SerializeField] private TextMeshProUGUI _healthText;
    
    private Transform _cameraTransform;
    
    private void Awake() => CacheComponents();
    private void LateUpdate() => RotateGauge();
    
    
    private void CacheComponents()
    {
        _cameraTransform = Camera.main.transform;
    }

    public void RefreshHealthBar(int health, int maxHealth)
    {
        _healthBar.fillAmount = health / (float)maxHealth;
        _healthText.text = $"{health}/{maxHealth}";
    }

    private void RotateGauge()
    {
        _guageTransform.forward = _cameraTransform.forward;
    }
}
