using System;
using System.Collections.Generic;
using UnityEngine;

public interface IPool
{
    //Ǯ���� ���ѳ�
    //Ǯ�� �ʿ��� ��� �ۼ�

    //�ϴ� Ǯ�� ������ġ
    Transform Trans { get; set; }

    //Ǯ�� ������Ʈ�� ������ �ڷᱸ��
    Queue<GameObject> Pool { get; set; }

    //Ǯ���� �޾ƿ���
    GameObject Get (Action<GameObject> action = null);

    //Ǯ���� �ݳ��ϱ�
    void Release (GameObject obj, Action<GameObject> action = null);

}

public class PoolManager : MonoBehaviour
{
    // Ǯ ���� ���ִ� �޴��� � ����� �־�� �ұ�?
    // ���ο� ���� ������ �� ������Ʈ �����ؼ� Ǯ �־��ֱ�?

    //�ϴ� Ű,������ ��ųʸ� -> Ű�� ���ҽ� ������ ���� ������Ʈ �̸�

    public Dictionary<string, IPool> pool_dict = new Dictionary<string, IPool>();

    //���� ��Ʈ��(���ҽ� ���Ͽ� ���� ������Ʈ �̸�) �� Ű�� ���� IPool�� ������ ����� ������ IPool ��ȯ

    public IPool Pooling(string key)
    {
        // Ǯ ��ųʸ��� ���� Ű�� ������
        if (pool_dict.ContainsKey(key) == false)
        {
            // Ǯ �߰�
            AddPool(key);
        }
        if (pool_dict[key].Pool.Count <= 0) // Ǯ ��ųʸ��� ť ������ 0���� ������
        {
            // ť �߰�
            AddQueue(key);
        }
        // �� ��������� ���� ��ȯ ������ IPool �� ����� Ŭ���� (���⼭�� ObjectPool)��
        return pool_dict[key];
    }

    // Ǯ �߰� ����
    // Ǯ �߰��� ���� ������Ʈ ��ȯ
    // ���� ������Ʈ ����� �� ���� ������Ʈ�� ObjectPool ������Ʈ �ް� ���� Ű+Pool�� �̸�����
    public GameObject AddPool(string key)
    {
        // Ǯ ������ ������Ʈ ����
        GameObject obj = new GameObject($"{key} Pool");
        // Ǯ Ŭ���� ����
        ObjectPool pool = new ObjectPool();
        // ������Ʈ�� Ǯ Ŭ���� ������Ʈ�� �ֱ�xx ������Ʈ�� �ƴϤ� �ڽ����� �ֱ�
        pool_dict.Add(key, pool);
        pool.Trans = obj.transform;
        return obj;
    }

    //Ǯ�� �ִµ� ������Ʈ�� �����ϸ� Quene�� �߰� ����
    public void AddQueue(string key)
    {
        GameObject obj = Manager.Instance.ResorceInstantiate(key);
        obj.transform.parent = pool_dict[key].Trans;

        pool_dict[key].Release(obj);
    }


}
