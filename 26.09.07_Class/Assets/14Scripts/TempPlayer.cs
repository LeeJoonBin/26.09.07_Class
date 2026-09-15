using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class TempPlayer : MonoBehaviour
{
    public UnityEvent TempEvent;
    private int _health;

    public int Health
    {
        get=>_health;
        private set
        {
            _health = value;
            OnHealthChange?.Invoke(_health);
        }
    }
    public event Action<int> OnHealthChange;

    public ObsevableProperty<float> Exp = new(0);

    
    private void OnEnable()
    {
        
    }
    // 콜백
    /*public void TryLoadData(Action s, Action f)
    {
        if ()
        {
            s.Invoke();
        }
        else
        {
            f.Invoke();
        }
    }*/
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) TakeDamage(5);
        if (Input.GetKeyDown(KeyCode.Alpha2)) Heal(10);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Exp.Value += 20.5f;
        if (Input.GetKeyDown(KeyCode.Alpha4)) TempEvent?.Invoke();
    }


   
    public void TakeDamage(int damage)
    {
        Debug.Log("데미지 받음");
        Health -= damage;
    }

    public void Heal(int heal)
    {
        Debug.Log("회복함");
        Health += heal;
    }
}
