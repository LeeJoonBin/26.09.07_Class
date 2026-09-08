using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour, IInteractable
{
	
    public GameObject GameObject
    {
        get => gameObject; 
    }
	private void Outline _outline;

    private void Awake() => CacheComponents();
    private void Start() => Init();
    public void Targeting()
    {
        
    }

    public void Untargeting()
    {
        
    }
    public void Interact(IInteractor owner)
    {



        Destroy(gameObject);
    }

    private void Init()
    {
        _outline.enabled = false;
    }
    private void CacheComponents()
    {
        _outline = gameObject.GetComponet<Outline>();
    }
}
