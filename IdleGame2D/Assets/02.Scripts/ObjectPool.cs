using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool :  IPool
{
    // 오브젝트 만들어서 이 클래스 컴포넌트로 넣고 풀 역활을 할꺼임
    // 풀이 뭐냐? 오브젝트들 미리 생성해두고 소환 제거 처럼 보이게 하려고 데이터들 모아놓는 공간?

    public Transform Trans { get; set; }
    public Queue<GameObject> Pool { get; set; } = new Queue<GameObject>();

    public GameObject Get(Action<GameObject> action = null)
    {
        var obj = Pool.Dequeue(); //풀에 있는 오브젝트 하나 꺼내기
        obj.SetActive(true); // 그거 활성화

        if (action != null)
        {
            action.Invoke(obj);
        }
        return obj;
    }

    public void Release(GameObject obj, Action<GameObject> action = null)
    {
        Pool.Enqueue(obj);
        obj.SetActive(false);
        if (action != null)
        {
            action.Invoke(obj);
        }

    }
}
