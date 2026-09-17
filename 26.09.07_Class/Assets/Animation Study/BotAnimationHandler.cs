using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAnimationHandler : MonoBehaviour
{
    [SerializeField] private string _moveXParam;
    [SerializeField] private string _moveZParam;
    [SerializeField] private string _attackAnimParam;
    [SerializeField] private string _readyAnimParam;

    private int _moveX;
    private int _moveZ;
    private int _attack;
    private int _ready;
    private BotController _botController;
    private Animator _animator;

    private void Awake()
    {
        Init();
        CacheComponent();
    }

    private void OnEnable()=> BindBotEvent();
    private void OnDisable() => UnBindBotEvent();
    
    private void BindBotEvent()
    {
        _botController.OnMove += SetMoveAnim;
        _botController.OnAttack += SetAttackAnim;
        _botController.OnReady += SetReadyAnim;
    }

    private void UnBindBotEvent()
    {
        _botController.OnMove -= SetMoveAnim;
        _botController.OnAttack -= SetAttackAnim;
        _botController.OnReady -= SetReadyAnim;
    }

    private void SetMoveAnim(Vector2 movement)
    {
        _animator.SetFloat(_moveX, movement.x);
        _animator.SetFloat(_moveZ, movement.y);
    }
    private void SetAttackAnim() => _animator.SetTrigger(_attack);

    private void SetReadyAnim()
    {
        _animator.SetBool(_ready, true);
    }

    private void CacheComponent()
    {
        _botController = GetComponent<BotController>();
        _animator = GetComponent<Animator>();
    }
    private void Init()
    {
        _moveX = Animator.StringToHash(_moveXParam);
        _moveZ = Animator.StringToHash(_moveZParam);
        _attack = Animator.StringToHash(_attackAnimParam);
        _ready = Animator.StringToHash(_readyAnimParam);
    }
}
