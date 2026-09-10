using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class TurretController : MonoBehaviour, IDamageable
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _cooldown;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private float _turretHealth;
    [SerializeField] private float _maxTurretHealth;
    [SerializeField] private GameObject _flameEffectPrefab;
    [SerializeField] private TextMeshProUGUI _destroyText;
    public GameObject GameObject { get => gameObject; }
    public float TurretHealth => _maxTurretHealth;
    public float MaxTurretHealth => _maxTurretHealth;
    private float _respawnTimer = 3f;
    public LayerMask TargetLayer;
    [Header("Bullet")]
    [SerializeField] private BulletController _bulletPrefab;
    [SerializeField] private int _bulletDamage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletDestoryDelay;
    
    
    private Transform _playerTransform;
    private float _currentCooldown;
    private bool _isPlayerInTrigger => _playerTransform != null;
    private bool _isPlayerInsight;
    private bool _isReadyToFire => _currentCooldown >= _cooldown;
    private SphereCollider _sphereCollider;
    private Transform _transform;
    private IDamageable _damageableImplementation;

    private void Awake() => CacheComponents();
    private void Update()
    {
        UpdateCurrentCooldown();
        RayShotToPlayer();
        Rotate();
        Fire();
    }

    

    private void CacheComponents()
    {
        _sphereCollider = GetComponent<SphereCollider>();
    }

    private void Fire()
    {
        if (!_isPlayerInsight || !_isPlayerInTrigger) return;
        
        Vector3 look = new Vector3(
            _playerTransform.position.x,
            _headTransform.position.y,
            _playerTransform.position.z);
        
        _headTransform.LookAt(look);
        
        if(!_isReadyToFire) return;
        
        spawnBullet();
        _currentCooldown = 0f;
    }

    private void UpdateCurrentCooldown()
    {
        if(_isReadyToFire) return;
        
        _currentCooldown += Time.deltaTime;
    }
    private void Rotate()
    {
        if (_isPlayerInsight) return;
     
        _headTransform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }

    private void spawnBullet()
    {
        // 프리팹
        // Instantiate 
        BulletController bullet = Instantiate(
            _bulletPrefab,
            _muzzlePoint.position,
            _muzzlePoint.rotation);
        
        bullet.SetData(_bulletDamage, _bulletSpeed, _bulletDestoryDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (TargetLayer.Contains(other))
        {
            _playerTransform = other.transform;
            Debug.Log("찾음");
        }*/
         int layer = (1 << other.gameObject.layer);
         
         if ((TargetLayer.value & layer) != 0)
         {
            _playerTransform = other.gameObject.transform;
            Debug.Log("찾음");
         }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerTransform = null;
        }
    }
    private void RayShotToPlayer()
    {
        _isPlayerInsight = false;
        if (!_isPlayerInTrigger) return;

        Vector3 from = new Vector3(
            transform.position.x,
            transform.position.y + _muzzlePoint.position.y,
            transform.position.z);
        
        Vector3 to = new Vector3(
            _playerTransform.position.x,
            _playerTransform.position.y + _muzzlePoint.position.y,
            _playerTransform.position.z);
            
        Ray ray = new Ray(from, (to - from).normalized);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, _sphereCollider.radius, TargetLayer))
        {
                _isPlayerInsight = true;
        }
    }

    private void PlayFlameEffect()
    {
        Instantiate(_flameEffectPrefab, _muzzlePoint.position, _muzzlePoint.rotation);
        Destroy(gameObject);
    }
    

    public void TakeDamage(int damage)
    {
        _turretHealth -= damage;
        Debug.Log(_turretHealth);
        if (_turretHealth <= 0)
        {
            PlayFlameEffect();
        }
        
    }

    private void PrintText()
    {
        _destroyText.text = $"<color=red>!터렛 파괴!</color>";
    }
}
