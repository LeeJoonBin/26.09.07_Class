using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private Transform _cameraTransform;

    [SerializeField] private KeyCode _fireKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _reloadKey = KeyCode.R;
    [SerializeField] private float _range;
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;
    [SerializeField] private int _maxMagazineCount;
    
    
    //-----------
    private int _currentMagazineCount;
    private bool _isPressReload => Input.GetKey(_reloadKey);
    // ---------
    private float  _currentCooldown;
    private bool _isPressedFire => Input.GetKeyDown(_fireKey);
    private bool _isReadyFire => _currentCooldown <= _cooldown;
    private bool _hasBullet => _currentMagazineCount > 0;
    private bool _canFire => _isPressedFire && _isReadyFire && _hasBullet;
    
    // 30
    // 리로드 R
    // 무제한
    private void Awake() => CacheComponents();
    private void Start() =>  Init();

    private void Update()
    {
        UpdateCooldown();
    }

    public void Reload()
    {
        if(!_isPressReload) return;
        _currentMagazineCount = _maxMagazineCount;
        Debug.Log($"Reload: {_currentMagazineCount}");
    }

    public void Fire()
    {
        if (!_canFire) return;
        _currentCooldown = 0f;
        _currentMagazineCount--;
        if (!TryGetDamageable(out IDamageable damageable)) return;
        damageable.TakeDamage(_damage);
        Debug.Log($"Player: {damageable.GameObject.name}에게 발사");
        Debug.Log($"{_currentMagazineCount}");
    }

    
    private bool TryGetDamageable(out IDamageable damageable)
    {
        bool result = false;
        damageable = null;
        
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, _range))
        {
            result = hit.transform.TryGetComponent(out damageable);
        }

        return result;
    }

    private void CacheComponents()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void UpdateCooldown()
    {
        if (_isReadyFire)  return;
        _currentCooldown += Time.deltaTime;
    }

    private void Init()
    {
        _currentCooldown = 0f;
        _currentMagazineCount = _maxMagazineCount;
    }
}
