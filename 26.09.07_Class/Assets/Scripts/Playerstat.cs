using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerstat : MonoBehaviour
{
    [field: SerializeField] public int Health { get; set; }
    [field: SerializeField] public float MoveSpeed { get; set; }
    [field: SerializeField] public float InteractRange { get; set; }
    
    [field: SerializeField] public float WeaponRange { get; set; }
    [field: SerializeField] public int Damage { get; set; }
    [field: SerializeField] public float WeaponCoolDown { get; set; }
    
}
