using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    /*
     공통 사항
     - 지속시간
     
     종류
     - 데미지 증가
     - 스피드증가
     - 연사속도 증가
     - 
     */
    
    private float _elapsedTime;

    private int _damage;
    private float _delay;
    private float _originalMoveSpeed;
    private float _moveSpeed;
    private float _originalFireCooldown;
    private float _fireCooldown;
    private PlayerController _controller;
    private PlayerWeapon _weapon;

    private void Update()
    {
        
    }

    public Buff SetDamage(int damage)
    {
        _damage =  damage;
        return this;
    }

    public Buff SetFirecooldown(float cooldown)
    {
        return null;
    }
}
