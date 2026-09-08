using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameEffect : MonoBehaviour
{
    [SerailizeField] private float _deactivateDelay;
    private float _elapsedTime;
    
    private void OnEnable() => ResetElapsedTime();
    private void Start() => gameObject.SetActive(false);

    private void Update()
    {
        UpdatyeElapsedTime();
        Deactivate();

    }


}
