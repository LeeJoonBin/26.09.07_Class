using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    [SerializeField] private KeyCode _throwKey = KeyCode.Alpha3;
    [SerializeField] private GrenadaMovement _grenadePrefab;
    [SerializeField] private float _maxKeydown;
    [SerializeField] private Transform _muzzlePoint;
    
    [SerializeField]private float _grenadeSpeed;
    [SerializeField]private float _grenadeDestoyTime;

    private float _countKeydown;
    private float _plusSpeed;

    private void Update()
    {
        
        ThrowGrenade();
        
    }

    private void ThrowGrenade()
    {
        if (!Input.GetKeyDown(_throwKey)) return;
        
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

    private void spawnGrenade()
    {
        
        GrenadaMovement grenade = Instantiate(_grenadePrefab,_muzzlePoint.position, _muzzlePoint.rotation);
        grenade.SetData( _grenadeSpeed , _grenadeDestoyTime);
    }
}
