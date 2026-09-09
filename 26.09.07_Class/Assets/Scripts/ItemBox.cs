using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemBox : MonoBehaviour, IInteractable
{
    [Header("Stimpack Settings")]
    [SerializeField] private float _buffDuration = 10f;
    [SerializeField] private float _speedBoost = 10f;
    [SerializeField] private float _fireRateBoost = 0.1f;
    [SerializeField] private float _cooldown = 0f;
    public GameObject GameObject { get => gameObject; }
    private Outline _outline;
    
    private void Awake() => CacheComponents();
    private void Start() => Init();
    
    public void Targeting()
    {
        _outline.enabled = true;
    }

    public void Untargeting()
    {
        _outline.enabled = false;
    }
    
    public void Interact(IInteractor owner)
    {
        GameObject own;
        if (owner.GameObject.CompareTag("Player"))
        {
            own = GameObject.Find("Player");
            own.GetComponent<PlayerMovement>().AddSpeed(_speedBoost);
            
            Destroy(gameObject);
        }
        // onwer의 능력치를 상승시킨다던가...
        // 인벤토리로 들어간다던가....
        // 무기가 생긴다던가...
        // 장탄수를 리필해준다던가...

        Destroy(gameObject);
    }
    
    private void Init()
    {
        _outline.enabled = false;
    }

    private void CacheComponents()
    {
        _outline = gameObject.GetComponent<Outline>();
    }
}
