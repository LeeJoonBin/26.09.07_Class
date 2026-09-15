using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TurretController : MonoBehaviour, IDamageable
{
    [Header("Turret")]
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _cooldown;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private Transform _muzzlePoint;
    
    
    // 터렛 파괴 이팩트
    [SerializeField] private GameObject _flameEffectPrefab;
    // 터렛 체력
    [SerializeField] private int _turretHealth;
    [SerializeField] private int _maxTurretHealth;
    // 터렛 UI불러올 때 쓸 것 --------
    public float TurretHealth => _turretHealth;
    public float MaxTurretHealth => _maxTurretHealth;
    //-------------
    public LayerMask TargetLayer;
    public GameObject GameObject { get => gameObject; }
    
    [Header("Bullet")]
    [SerializeField] private ObjectPool _bulletPool;
    [SerializeField] private BulletController _bulletPrefab;
    [SerializeField] private int _bulletDamage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletReturnDelay;
    // UI
    private EnemyUIController _ui;
    
    private Transform _playerTransform; // Player포지션
    private float _currentCooldown; // 불렛발사 쿨타임계산할 때 필요
    private bool _isPlayerInTrigger => _playerTransform != null;
    private bool _isPlayerInsight;
    private bool _isReadyToFire => _currentCooldown >= _cooldown;
    private SphereCollider _sphereCollider;  // 이거 왜 필요한지 아직 잘 모르겠음
    private Transform _transform; // 이것도? 사용된적이없는데 한번 나중에 수정해봐야할듯
    private IDamageable _damageableImplementation; // ?? 이것도

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
        _ui = GetComponent<EnemyUIController>();
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
        // 1. 얻어오기
        IPoolable bullet = _bulletPool.Take();
        
        // 2. Transform.position, rotation 설정
        bullet.tr.position = _muzzlePoint.position;
        bullet.tr.rotation = _muzzlePoint.rotation;
        
        // 3 . 활성화 
        bullet.tr.gameObject.SetActive(true);
        /*BulletController bullet = Instantiate(
            _bulletPrefab,
            _muzzlePoint.position,
            _muzzlePoint.rotation);
            */
        
        //getcomponent말고 비교적 연산이 적은 것으로 불러오자
        (bullet as BulletController).SetData(_bulletDamage, _bulletSpeed, _bulletReturnDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        /* 메서드확장자 사용해서 한것
        if (TargetLayer.Contains(other))
        {
            _playerTransform = other.transform;
            Debug.Log("찾음");
        }
        */
         int layer = (1 << other.gameObject.layer);
         
         if ((TargetLayer.value & layer) != 0)
         {
            _playerTransform = other.gameObject.transform;
         }
    }

    private void OnTriggerExit(Collider other)
    {
        int layer = (1 << other.gameObject.layer);
        if ((TargetLayer.value & layer) !=0)
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

    private void Init()
    {
        _turretHealth = _maxTurretHealth;
        _ui.RefreshHealthBar(_turretHealth, _maxTurretHealth);
    }

    public void TakeDamage(int damage)
    {
        _turretHealth -= damage;
        _ui.RefreshHealthBar(_turretHealth, _maxTurretHealth);
        Debug.Log(_turretHealth);
        if (_turretHealth <= 0)
        {
            PlayFlameEffect();
        }
    }
}
