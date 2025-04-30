using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool :  IPool
{
    // ������Ʈ ���� �� Ŭ���� ������Ʈ�� �ְ� Ǯ ��Ȱ�� �Ҳ���
    // Ǯ�� ����? ������Ʈ�� �̸� �����صΰ� ��ȯ ���� ó�� ���̰� �Ϸ��� �����͵� ��Ƴ��� ����?

    public Transform Trans { get; set; }
    public Queue<GameObject> Pool { get; set; } = new Queue<GameObject>();

    public GameObject Get(Action<GameObject> action = null)
    {
        var obj = Pool.Dequeue(); //Ǯ�� �ִ� ������Ʈ �ϳ� ������
        obj.SetActive(true); // �װ� Ȱ��ȭ

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
