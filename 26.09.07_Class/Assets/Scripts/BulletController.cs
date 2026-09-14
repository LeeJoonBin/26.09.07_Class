using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour , IPoolable
{
    private float _returnDelay;
    private float _elapsedTime;
    private float _damage;
    private float _speed;
    public ObjectPool Pool { get; set; }
    public Transform tr { get => transform; }
    // 어딘가에 부딪히면
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            // 데미지 추가
            Debug.Log("플레이어 맞음");
        }
        Pool.Return(this);
    }
        
        // 벽인 경우 파괴

        private void Update()
        {
            UpdateElapsedTime();
            MoveForward();
            ReturnToPool();
        } 
    public void ReturnToPool()
    {
            // 제한시간이 경과할 것.
        if (_elapsedTime >= _returnDelay)
        {
            // 자신이 속한 풀에대한 참조
            // 풀 내부적으로 다시 오브젝트를 넣어놓는 기능
            _elapsedTime = 0;
            Pool.Return(this);
        }
    }
    
    // 앞으로 전진
    private void MoveForward()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    
    // 터렛으로부터 데이터 전달 받기.
    public void SetData(int damage, float speed, float returnDelay)
    {
        _damage = damage;
        _speed = speed;
        _returnDelay = returnDelay;
        _elapsedTime = 0f;
    }

    private void UpdateElapsedTime()
    {
        _elapsedTime += Time.deltaTime;
    }
}
