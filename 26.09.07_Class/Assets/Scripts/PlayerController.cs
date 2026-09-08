using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IInteractor
{
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _detectionRange;
    [SerializeField] private KeyCode _interactionKey = KeyCode.E;
    
    private PlayerWeapon _weapon;
    private PlayerMovement _movement;
    private Transform _cameraTransform;
    
    private IInteractable _targetnteractable;
    private bool _hasDetectInteractable => _targetnteractable != null;
    private bool _isPressedInteractionKey => Input.GetKeyDown(_interactionKey);
    private bool _canInteraction => _hasDetectInteractable && _isPressedInteractionKey;
    
    public GameObject GameObject {get => gameObject;}
    // ------------------------------------
    private void Awake() => CacheComponents();
    private void Start() => LockCursor();
    private void FixedUpdate() => _movement.Move();

    private void Update()
    {
        _movement.Rotate();
        _weapon.Fire();
        _weapon.Reload();
        DetectInteractable();
        TryInteract();
    }

    private void LateUpdate()
    {
        SetWeaponTransform();
        SetCamraTransform();
    }
    // ----------------------------------
    
    private void CacheComponents()
    {
        _movement = GetComponent<PlayerMovement>();
        _weapon = GetComponentInChildren<PlayerWeapon>();
        _cameraTransform = Camera.main.transform;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void SetWeaponTransform()
    {
        _weapon.transform.SetPositionAndRotation(
            _cameraPivot.position,
            _cameraPivot.rotation
            );
    }
    private void SetCamraTransform()
    {
        _cameraTransform.SetPositionAndRotation(_cameraPivot.position, _cameraPivot.rotation);
        // _cameraPivot.position = _cameraTransform.position;
        // _cameraPivot.rotation = _cameraTransform.rotation;
    }

    public void DetectInteractable()
    {   
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        RaycastHit hit;
        
        if (!Physics.Raycast(ray, out hit,  _detectionRange))
        {
            
            return;
        }
        if (_hasDetectInteractable)
        {
            if (hit.collider.gameObject == _targetnteractable.GameObject)
            {
                return;
            }
            
        }
        
        _targetnteractable = hit.collider.GetComponent<IInteractable>();
        
        if(_hasDetectInteractable) Debug.Log($"{_targetnteractable.GameObject.name} 감지");
    }
    public void TryInteract()
    {
        if(!_canInteraction) return;

        _targetnteractable.Interact(this);
        _targetnteractable = null;
    }
}
