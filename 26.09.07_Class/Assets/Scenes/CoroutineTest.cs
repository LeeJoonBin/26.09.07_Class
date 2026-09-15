using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    [SerializeField]  private float _delay;
    private WaitForSeconds _wait;
    private bool _isbool = true;
    private Coroutine _routine;
    
    private void Awake()
    {
        _wait = new WaitForSeconds(_delay);
    }

    private void Start()
    {
        Debug.Log("Start 시작");
       
        Debug.Log("Start 종료");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Run();
        if (Input.GetKeyDown(KeyCode.Alpha2)) Stop();
    }
    private void Run()
    {
        if(_routine != null) return;
        
        _routine = StartCoroutine(MyRoutine());
    }

    private void Stop()
    {
        if (_routine == null) return;
        
        StopCoroutine(_routine);
        _routine = null;
    }
    // 함수의 반환형은 'IEnumerator'
    private IEnumerator MyRoutine()
    {
        /*
        // 버프 발동
        yield return new WaitForSeconds(10f);
        // 버프 종료
        */
        while (true)
        {
            // 반환형이여서 무언가를 반환해야된는데
            // 이때 반환할 때는 'yield return'을 사용한다.
            // yield return OOO : OOO이(가) 충족되는 상황까지 함수를 일시정지하고 대기할 것.
            //yield return new WaitUntil(() => _isbool);
            
            yield return _wait;
            Debug.Log("Coroutine");
            // 루틴을 아예 멈출 떄
            // yield break;
        }
    }
    
    /*
    private float _elapsedTime;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _time)
        {
            _elapsedTime = 0;
            Debug.Log("지정 시간 경과");
        }
    }
    */

}
