using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BotController : MonoBehaviour
{
    public event Action<Vector2> OnMove;
    public event Action OnAttack;
    public event Action OnReady; 

    private Vector2 _prevMovement;
    private void Update()
    {
        SetMove();
        if (Input.GetKeyDown(KeyCode.Space)) Attack();
        if (Input.GetKeyDown(KeyCode.Mouse0)) Ready();
    }

    private void SetMove()
    {
        Vector2 movement = GetMovement();
        if(_prevMovement == movement)return;
        
        OnMove?.Invoke(movement);
        _prevMovement = movement;
        //이전 프레임의 Movement와 같으면 return;
        // 다르다면 OnMove실행 + _prevMovement갱신
    }
    private void Ready() => OnReady?.Invoke();
    
    private Vector2 GetMovement()
    {
        //입력받아서  Vector2반환
        //단위백터로 만들지마세요
        return new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));
    }

    private void Attack()
    {
        Debug.Log("Attack");
        OnAttack?.Invoke();
    }
}
