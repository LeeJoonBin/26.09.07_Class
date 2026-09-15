using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    
    [SerializeField] private GameObject _prefab;// Object에 담을 Gameobject
    private IPoolable[] _pool; // GameObject를 몇 개까지 만들지 설정하기 위한 배열
    [field: SerializeField] public int Size{get; private set;} // 위에 배열의 크기를 인스팩터에서 설정하기 위한 것
    public int Count{get; private set;} // 메서드에서 Size를 계산하기 위해서?
    public bool IsEmpty => Count == 0;
    public void Awake() => Init();

    public IPoolable Take()
    {
        if (IsEmpty) return null;    
        
        Count--;
        IPoolable poolable = _pool[Count];
        _pool[Count] = null;
        
        return poolable;
    }

    public void Return(IPoolable poolable)
    {
        if(Size <= Count) return;
        
        _pool[Count] = poolable;
        poolable.tr.gameObject.SetActive(false);
        Count++;
        
    }
    private void Init()
    {
        _pool = new IPoolable[Size];
        
        for (int i = 0; i < _pool.Length; i++)
        {
            GameObject go = Instantiate(_prefab);
            _pool[i] = go.GetComponent<IPoolable>();
            _pool[i].Pool = this;
            go.SetActive(false);
        }
        Count = Size;
    }
}
