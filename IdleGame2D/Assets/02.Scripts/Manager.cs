using System;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // �̱���
    // �̹� �ν��Ͻ��� ������ �������� 1���� �ν��Ͻ��� �־���Ѵ�
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

    //Ǯ �޴��� ���
    private static PoolManager PoolManager = new PoolManager();


    //�������� ������Ƽ
    public static PoolManager Pool { get => PoolManager; }



    // ��ü ������� �޼���
    public GameObject ResorceInstantiate(string key)
    {
        return Instantiate(Resources.Load<GameObject>(key));
    }
}
