using System;
using System.Collections.Generic;
using UnityEngine;

public interface IPool
{
    //풀링을 위한놈
    //풀이 필요한 기능 작성

    //일단 풀의 생성위치
    Transform Trans { get; set; }

    //풀에 오브젝트들 관리할 자료구조
    Queue<GameObject> Pool { get; set; }

    //풀에서 받아오기
    GameObject Get (Action<GameObject> action = null);

    //풀에다 반납하기
    void Release (GameObject obj, Action<GameObject> action = null);

}

public class PoolManager : MonoBehaviour
{
    // 풀 관리 해주는 메니저 어떤 기능이 있어야 할까?
    // 새로운 값이 들어오면 빈 오브젝트 생성해서 풀 넣어주기?

    //일단 키,벨류쌍 딕셔너리 -> 키는 리소스 파일의 게임 오브젝트 이름

    public Dictionary<string, IPool> pool_dict = new Dictionary<string, IPool>();

    //들어온 스트링(리소스 파일에 게임 오브젝트 이름) 을 키로 같는 IPool이 없으면 만들고 있으면 IPool 반환

    public IPool Pooling(string key)
    {
        // 풀 딕셔너리에 같은 키가 없으면
        if (pool_dict.ContainsKey(key) == false)
        {
            // 풀 추가
            AddPool(key);
        }
        if (pool_dict[key].Pool.Count <= 0) // 풀 딕셔너리의 큐 갯수가 0보다 작으면
        {
            // 큐 추가
            AddQueue(key);
        }
        // 다 통과했으면 벨류 반환 벨류는 IPool 을 상속한 클래스 (여기서는 ObjectPool)임
        return pool_dict[key];
    }

    // 풀 추가 로직
    // 풀 추가된 게임 오브젝트 반환
    // 게임 오브젝트 만들고 그 게임 오브젝트에 ObjectPool 컴포넌트 달고 들어온 키+Pool로 이름짓기
    public GameObject AddPool(string key)
    {
        // 풀 저장할 오브젝트 만듦
        GameObject obj = new GameObject($"{key} Pool");
        // 풀 클래스 만듦
        ObjectPool pool = new ObjectPool();
        // 오브젝트에 풀 클래스 컴포넌트로 넣기xx 컴포넌트가 아니ㅏ 자식으로 넣기
        pool_dict.Add(key, pool);
        pool.Trans = obj.transform;
        return obj;
    }

    //풀이 있는데 오브젝트가 부족하면 Quene에 추가 로직
    public void AddQueue(string key)
    {
        GameObject obj = Manager.Instance.ResorceInstantiate(key);
        obj.transform.parent = pool_dict[key].Trans;

        pool_dict[key].Release(obj);
    }


}
