using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIBinder : MonoBehaviour
{
    private TempPlayer _player;
    [SerializeField]private TempPlayerUI _playerUI;
    [SerializeField]private HealthGauge _healthGauge;
    [SerializeField] private ExpGauge _expGauge;

    private void Awake() => CacheComponents();
    private void OnEnable() => BindPlayerStatChangeEvent();
    private void OnDisable() => UnBindPlayerStatChangeEvent();

    private void OnDestroy() => _player.Exp.RemoveAllListeners();
    private void BindPlayerStatChangeEvent()
    {
        _player.OnHealthChange += _playerUI.RefreshHealthUI;
        _player.OnHealthChange += _healthGauge.RefreshGauge;
        
        _player.Exp.AddListener(_expGauge.RefreshGauge);
    }

    private void UnBindPlayerStatChangeEvent()
    {
        _player.OnHealthChange -= _playerUI.RefreshHealthUI;
        _player.OnHealthChange -= _healthGauge.RefreshGauge;
        
        _player.Exp.RemoveListener(_expGauge.RefreshGauge);
    }
    private void CacheComponents()
    {
        _player = GetComponent<TempPlayer > ();
    }
}
