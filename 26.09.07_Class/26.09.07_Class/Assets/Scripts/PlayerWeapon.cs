using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    
    private Transform _cameraTransform;

    [SerializeField] private KeyCode _fireKey = KeyCode.Mouse0;
    [SerializeField] private float _range;
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;
    
    private float  _currentCooldown;
    private bool _isPressedFire => Input.GetKeyDown(_fireKey);
    private bool _isReadyFire => _currentCooldown >= _cooldown;
    private bool _canFire => _isPressedFire && _isReadyFire;
    private void Awake() => CacheComponents();
    private void Start() =>  Init();

    private void Update()
    {
        UpdateCooldown();
    }

    public void Fire()
    {
        if (!_canFire) return;
        
        if (!TryGetDamageable(out IDamageable damageable)) return;
        
        damageable.TakeDamage(_damage);
        _currentCooldown = 0f;
        Debug.Log($"Player: {damageable.GameObject.name}에게 발사");
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
        _currentCooldown = 1f;
    }
}
