using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _cooldown;
    [SerializeField] private Transform _headTransform;
    [SerializeField] private Transform _muzzlePoint;
    
    [Header("Bullet")]
    [SerializeField] private BulletController _bulletPrefab;
    [SerializeField] private int _bulletDamage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletDestoryDelay;
    
    private Transform _playerTransform;
    private float _currentCooldown;
    private bool _isPlayerInTrigger => _playerTransform != null;
    private bool _isPlayerInsight = false;
    private bool _isReadyToFire => _currentCooldown >= _cooldown;
    private SphereCollider _sphereCollider;

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
        if (other.CompareTag("Player"))
        {
            _playerTransform = other.transform;
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
            transform.position.y + _muzzlePoint.position.y / 2,
            transform.position.z);
        
        Vector3 to = new Vector3(
            _playerTransform.position.x,
            _playerTransform.position.y + _muzzlePoint.position.y / 2,
            _playerTransform.position.z);
            
        Ray ray = new Ray(from, (to - from).normalized);
        RaycastHit hit;

        // LaterMask 배운 후 리팩토링
        // 발사 높이와 감지 높이를 다르게 해서 우희
        
        
        if (Physics.Raycast(ray, out hit, _sphereCollider.radius))
        {
            // 플레이어 찾았으면 감지 완료된거임.
            if (hit.transform.CompareTag("Player"))
            {
                _isPlayerInsight = true;
                Debug.Log("플레이어 감지");
            }

        }
    }
    
    
}
