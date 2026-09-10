using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class FieldUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _objectName;
    [SerializeField] private Canvas _objectHealth;
    [SerializeField] private TakeInfo _takeInfo;
    
    private PlayerController _playerCameraPivot;
    private TurretController _turret;
    private PlayerWeapon _weapon;
    private void Start() => CacheComponents();

    private void Update()
    {
        TakeInfo();
    }

    private void CacheComponents()
    {
        _weapon = GetComponent<PlayerWeapon>();
        _turret = GetComponent<TurretController>();
    }

    public void TakeInfo()
    {
        if (_weapon.TryGetDamageable(out IDamageable damageable))
        {
            RefreshMagazineUI();
        }
    }
    public void RefreshMagazineUI()
    {
        Instantiate(Resources.Load("TakeInfo"), _objectHealth.transform);
    }

}
