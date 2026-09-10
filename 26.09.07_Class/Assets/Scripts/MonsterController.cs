using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private Transform _bodyTransform;
    public LayerMask TargetLayer;

    private Transform _playerTransform;
    private bool _isPlayerInRange => _playerTransform != null;
    private bool _isPlayerInsight = false;
    private SphereCollider _sphereCollider;
    private void Start(){}

    private void Update()
    {
        TargetPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        int layer = (1 << other.gameObject.layer);

        if ((TargetLayer.value & layer) != 0)
        {
            _playerTransform = other.gameObject.transform;
        }
        Debug.Log("플레이어 감지");
    }

    private void OnTriggerExit(Collider other)
    {
        int layer = (1 << other.gameObject.layer);
        if ((TargetLayer.value & layer) == 0)
        {
            _playerTransform = null;
        }
    }

    private void TargetPlayer()
    {
        if (!_isPlayerInsight || !_isPlayerInRange) return;
        Vector3 look = new Vector3(
            _playerTransform.position.x, 
            _bodyTransform.position.y, 
            _playerTransform.position.z);
        
        _bodyTransform.LookAt(look);
        Debug.Log("찾음");
        MoveTowardsPlayer();
        
    }
    private void MoveTowardsPlayer()
    {
        
        
        /*Vector3 direction = look - transform.position;
        
        Vector3 newVelocity = direction.normalized * _moveSpeed*/;
    }
}
