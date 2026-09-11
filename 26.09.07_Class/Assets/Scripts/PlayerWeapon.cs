using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private Transform _cameraTransform;

    [SerializeField] private KeyCode _fireKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _reloadKey = KeyCode.R;
    [SerializeField] private int _maxMagazine;
    [SerializeField] private FlameEffect _flameEffect;
    [SerializeField] private FlameEffect _bulletFlameEffectPrefab;
    
    [Header("Grenade")]
    [SerializeField] private KeyCode _throwKey = KeyCode.Alpha3;
    [SerializeField] private int _maxGrenade;
    [SerializeField] private float _maxGrenadeThrowForce; //3
    [SerializeField] private Transform _grenadeMuzzlePoint;
    [SerializeField] private float _currentGrenadeThrowForce;
    
    public LayerMask TargetLayer;
    private int _damage => _stats.Damage;
    private float _cooldown => _stats.WeaponCoolDown;
    private float _range => _stats.WeaponRange;
    private Playerstat _stats;
    private float _currentCooldown;
    private int _currentMagazine;
    public int CurrentMagazine => _currentMagazine;
    public int MaxMagazine => _maxMagazine;
    private bool _isUpGrenadeKey => Input.GetKeyDown(_throwKey);
    private bool _isPressedFire => Input.GetKey(_fireKey);
    private bool _isPressedReload => Input.GetKeyDown(_reloadKey);
    private bool _hasGrenade => _maxGrenade > 0;
    private bool _hasEnoughForce => _currentMagazine >= _maxMagazine * 0.3;
    private bool _canThrowGrenade => _isUpGrenadeKey && _hasGrenade;
    private bool _isReadyFire => _currentCooldown >= _cooldown;
    private bool _hasBullets => _currentMagazine > 0;
    private bool _canFire => _isPressedFire && _isReadyFire && _hasBullets;
    private bool _canThrow => _canThrowGrenade && _hasEnoughForce;
    
    // ------------------------------------------
    private void Awake() => CacheComponents();
    private void Start() => Init();
    private void Update() => UpdateCooldown();
    // ------------------------------------------

    public void Fire()
    {
        if (!_canFire) return;
        
        _currentMagazine--;
        _currentCooldown = 0f;
        PlayFlameEffect();
        

        if (!TryGetDamageable(out IDamageable damageable)) return;
        
        damageable.TakeDamage(_damage);
    }
    /*public void Fire(float fireRate)
    {
        if (!_canFire) return;
        _cooldown /= fireRate;
        _currentMagazine--;
        _currentCooldown = 0f;
        PlayFlameEffect();

        if (!TryGetDamageable(out IDamageable damageable)) return;
        
        damageable.TakeDamage(_damage);
        Debug.Log($"{_currentMagazine}");
    }*/

    private void PlayFlameEffect()
    {
        _flameEffect.gameObject.SetActive(true);
        _flameEffect.Play();
    }

    private void PlayBulletImpactEffect(RaycastHit hit)
    {
        Transform effectTransform = Instantiate(_bulletFlameEffectPrefab).transform;
        effectTransform.position = hit.point;
        effectTransform.forward = hit.normal;
    }
    public bool TryGetDamageable(out IDamageable damageable)
    {
        bool result = false;
        damageable = null;
        
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, _range, TargetLayer))
        {
            PlayBulletImpactEffect(hit);
            result = hit.transform.TryGetComponent(out damageable);
        }

        return result;
    }

    private void CacheComponents()
    {
        _stats = GetComponent<Playerstat>();
        _cameraTransform = Camera.main.transform;
    }

    private void Init()
    {
        _currentCooldown = 0f;
        _currentMagazine = _maxMagazine;
    }

    private void UpdateCooldown()
    {
        if (_isReadyFire) return;
        
        _currentCooldown += Time.deltaTime;
    }

    public void Reload()
    {
        if (!_isPressedReload) return;
        
        _currentMagazine = _maxMagazine;
    }
}
