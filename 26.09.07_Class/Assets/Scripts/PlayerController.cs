using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private PlayerWeapon _weapon;
    
    private PlayerMovement _movement;
    private Transform _cameraTransform;
    
    // ------------------------------------
    private void Awake() => CacheComponents();

    private void Start() => LockCursor();
    private void FixedUpdate() => _movement.Move();

    private void Update()
    {
        _movement.Rotate();
        _weapon.Fire();
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
}
