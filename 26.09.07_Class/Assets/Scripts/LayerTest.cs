using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerTest : MonoBehaviour
{
    [SerializeField] private float _raycastDistance;
    public LayerMask TargetLayer;
    // or연산자는 레이어들을 추가 해줄 때
    // and연산자는 내가 원하는 레이어를 찾을 때

    private void Start()
    {
        TargetLayer = TargetLayer.Remove(9);
    }
    private void OnTriggerEnter(Collider other)
    {
        /*int layer = (1 << other.gameObject.layer);   
        
        if ((TargetLayer.value & layer) != 0)
        {
            Debug.Log("찾았다.");            
        }*/
        if (TargetLayer.Contains(other))
        {
            Debug.Log("찾았다.");
        }

    }

    private bool COntainsLayer(LayerMask mask, Collider layer)
    {
        return 0 != (mask.value &(1 << layer.gameObject.layer));
    }
    public void Update()
    {
        
        // 이 오브젝트의 위치에서 정면으로 발사되는 Ray 생성.
        // 레이케스트를 사용해서 감지된 게임 오브젝트의 이름을 출렸한다.
        // 레이케스트의 거리는 인스팩터에서 조절할 수 있도록 한다.
        /*Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _raycastDistance, TargetLayer))
        {
            Debug.Log(hit.transform.name);
        }*/
    }
}

