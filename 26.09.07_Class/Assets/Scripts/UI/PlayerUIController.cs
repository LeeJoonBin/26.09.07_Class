using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{ 
    [SerializeField] private TextMeshProUGUI _magazine;
    [SerializeField] private Image _playerHealth;
    
    private PlayerController _player;
    private PlayerWeapon _weapon;
    private void Awake() => CacheComponents();
    private void CacheComponents()
    {
        _weapon = GetComponentInChildren<PlayerWeapon>();
    }
    
    private void Update()
    {
        RefreshMagazineUI();
    } 

    public void RefreshMagazineUI()
    {
        _magazine.text = $"{_weapon.CurrentMagazine} / {_weapon.MaxMagazine}";
        
    }

    public void RefreshPlayerHealthUI()
    {
        _playerHealth.fillAmount = _player.CurrentHealth / _player.MaxHealth;
    }
    /*
    [SerializeField] private TextMeshProUGUI _health;
    
    private PlayerController _currentHealth;
    
    private void CacheComponents()
    {
        _weapon = GetComponentInChildren<PlayerWeapon>();
        _currentHealth = GetComponent<PlayerController>();
    }

    public void RefreshHealthUI()
    {
        _health.text = $"{_currentHealth.CurrentHealth} / {_currentHealth.MaxHealth}";
    }*/
}
