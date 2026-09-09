using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    [SerializeField]private float _grenadeDestoyTime;
    
    [SerializeField] private GrenadaMovement _grenadePrefab;
    [SerializeField] private FlameEffect _flameEffect;
    
    private Rigidbody _rigidbody;
    private float _countKeydown;
    private float _plusSpeed;
    

    private void Start() => _rigidbody = GetComponent<Rigidbody>();

    private void Force(Vector3 force)
    {
        _rigidbody.velocity = force;
    }
    /*private void Update()
    {
        
        ThrowGrenade();
        
    }

    private void ThrowGrenade()
    {
        if (!_canThrow) return;
        _maxGrenade--;
        spawnGrenade();
    }

    public void CountKeydown()
    {
        _plusSpeed = 0;
        
        if (Input.GetKey(_throwKey))
        {
            _plusSpeed++;
            if (_plusSpeed > _maxKeydown)
            {
                _plusSpeed = _maxKeydown;
            }
        }
    }

    private void GrenadeFlameEffect()
    {
        _flameEffect.gameObject.SetActive(true);
        _flameEffect.Play();
    }

    private void spawnGrenade()
    {
        GrenadeFlameEffect();
        GrenadaMovement grenade = Instantiate(_grenadePrefab,_muzzlePoint.position, _muzzlePoint.rotation);
        grenade.SetData( _grenadeSpeed , _grenadeDestoyTime, _flameEffect);
        
    }*/
}
