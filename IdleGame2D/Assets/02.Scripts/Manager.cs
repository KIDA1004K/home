using System;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // 싱글톤
    // 이미 인스턴스가 있으면 없에버림 1개의 인스턴스만 있어야한다
    public static Manager Instance = null;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //풀 메니저 등록
    private static PoolManager PoolManager = new PoolManager();


    //접근해줄 프로퍼티
    public static PoolManager Pool { get => PoolManager; }



    // 객체 생성담당 메서드
    public GameObject ResorceInstantiate(string key)
    {
        return Instantiate(Resources.Load<GameObject>(key));
    }
}
