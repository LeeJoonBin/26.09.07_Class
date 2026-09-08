using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadaMovement : MonoBehaviour
{
    private float _grenadeDamage;
    private float _grenadeSpeed;
    
    private void Update() => MoveForward();

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * _grenadeSpeed * Time.deltaTime);
    }

    public void SetData( float grenadeSpeed,  float grenadeDestoryDelay, FlameEffect flameEffect)
    {
        _grenadeSpeed = grenadeSpeed;
        
        Destroy(gameObject, grenadeDestoryDelay);
    }
}
