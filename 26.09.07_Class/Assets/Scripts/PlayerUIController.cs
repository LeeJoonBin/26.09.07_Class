using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _magazine;
    [SerializeField] private TextMeshProUGUI _health;
    private PlayerWeapon _weapon;
    private PlayerController _currentHealth;
    
    private void Awake() => CacheComponents();
    private void Update()
    {
        RefreshMagazineUI();
        RefreshHealthUI();
    } 

    private void CacheComponents()
    {
        _weapon = GetComponentInChildren<PlayerWeapon>();
        _currentHealth = GetComponent<PlayerController>();
    }
    public void RefreshMagazineUI()
    {
        _magazine.text = $"{_weapon.CurrentMagazine} / {_weapon.MaxMagazine}";
    }

    public void RefreshHealthUI()
    {
        _health.text = $"{_currentHealth.CurrentHealth} / {_currentHealth.MaxHealth}";
    }
}
