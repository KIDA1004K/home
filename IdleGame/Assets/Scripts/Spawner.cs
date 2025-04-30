//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class Spawner : MonoBehaviour
//{


//    // 1. ������ ������ ������ �� ���� ���ٴ� �� �������� �����Ǵ� ��찡 ����. (�� Ÿ��)

//    // �� �۾��� ����Ƽ������ �ڷ�ƾ �̶�� ������� �����Ϻ��.

//    // �ڷ�ƾ�� ���� ���¤Ӵ� ���
//    // 1. ���� ����
//    // 2. ����, ��ų ��Ÿ��
//    public int count;
//    public float spawnTime;
//    //public GameObject[] monster_prefabs;
//    //public static List<Monster> monster_list = new List<Monster>();
//    //public static List<Player> player_list = new List<Player>();
//    private void Start()
//    {
//        StartCoroutine(C_Spawn());
//    }



//    IEnumerator C_Spawn()
//    {
//        //1. ��� ������ ���ΰ�?
//        Vector3 pos;

//        //2. �󸶳� ������ ���ΰ�? 

//        for (int i = 0; i < count; i++)
//        {
//            // 3. � ���·� ������ ���ΰ�?
//            pos = Vector3.zero + Random.insideUnitSphere * 8.0f;
//            // y �� �������� �����ϱ� y=0����
//            pos.y = 0.0f;

//            //������ �Ÿ��� �������� �� ���� ����
//            while (Vector3.Distance(pos, Vector3.zero) <=5.0f)
//            {
//                pos = Vector3.zero + Random.insideUnitSphere * 8.0f;
//                pos.y = 0.0f;
//            }


//            //Action �븮�ڸ� Ȱ���� ���� Ǯ��
//            var go = Manager.Pool.pooling("Monster").get((value) =>
//            {
//                //�۾��� ����
//                value.GetComponent<Monster>().MonsterInit();
//                value.transform.position = pos;
//                value.transform.LookAt(Vector3.zero);

//            });
//            StartCoroutine(C_Realse(go));
//            //Instantiate(monster_prefabs[Random.Range(0, 3)],pos,Quaternion.identity);
//            // ���� ���� �� �ٽ� ���ƿ��� �ڵ�
//            // waitforseconds(float t) : �ۼ��� �� ��ŭ ����մϴ�.(�� ����)
//            //Random.Range(0,3)
//        }
//        yield return new WaitForSeconds(spawnTime);
//        StartCoroutine(C_Spawn());

//    }

//    IEnumerator C_Realse(GameObject obj)
//    {
//        // 1�� ���
//        yield return new WaitForSeconds(1.0F);
//        Manager.Pool.pool_dict["Monster"].Release(obj);
//    }
//}
using System.Collections;
using System.Collections.Generic; //List<T> ����� ���� �߰�
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 1. ������ ������ ������ �� ���� ���ٴ� �� ��������
    //    �����Ǵ� ��찡 ����.(�� Ÿ��)

    //  �� �۾��� ����Ƽ������ �ڷ�ƾ�̶�� ������� �����մϴ�.

    //�ڷ�ƾ�� ���� ���Ǵ� ���
    //1. ���� ����
    //2. ����, ��ų ��Ÿ��
    [SerializeField]private int count;          //������ ������ ����
    [SerializeField]private float spawnTime;    //���� �ֱ�(�� Ÿ��, ���� Ÿ��...)
    //public GameObject monster_prefab; //���� ������

    public static List<Monster> monster_list = new List<Monster>();
    public static List<Player> player_list = new List<Player>();
    //��ġ�� ���ӿ��� ĳ���͸� ���� �� ����ϴ� ��찡 �����ϱ� ����

    private void Start()
    {
        StartCoroutine(CSpawn());
        //StartCorountine(�Լ���());
    }
    IEnumerator CSpawn()
    {
        //1. ��� ������ ���ΰ�?
        Vector3 pos;
        //2. �� �� ������ ���ΰ�?
        for (int i = 0; i < count; i++)
        {
            //3. � ���·� ������ ���ΰ�?
            pos = Vector3.zero + Random.insideUnitSphere * 10.0f;
            //���� ���� ����(y ��ǥ = 0)
            pos.y = 0.0f;
            //������ �Ÿ��� �������� �� ���� ����
            while (Vector3.Distance(pos, Vector3.zero) <= 5.0f)
            {
                pos = Vector3.zero + Random.insideUnitSphere * 10.0f;
                pos.y = 0.0f;
            }
            //Instantiate(monster_prefab,pos,Quaternion.identity);

            //Action �븮�ڸ� Ȱ���� ���� Ǯ��
            //var go = Manager.Pool.pooling("Monster").get();

            var go = Manager.Pool.pooling("Monster").get((value) =>
            {
                value.GetComponent<Monster>().MonsterInit();
                value.transform.position = pos;
                value.transform.LookAt(Vector3.zero);
                monster_list.Add(value.GetComponent<Monster>());
            });
            //Quaternion.identity : ȸ�� �� 0
            //���� ���¸� �״�� �����ϴ� ��쿡 ����ϴ� ��

            //�ݳ�
            //StartCoroutine(CRelease(go));
        }

        //yield return : ���� ���� �� �ٽ� ���ƿ��� �ڵ�
        //WaitForSeconds(float t) : �ۼ��� �� ��ŭ ����մϴ�.(�� ����)
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(CSpawn());
    }

    IEnumerator CRelease(GameObject obj)
    {
        //1�� ���
        yield return new WaitForSeconds(1.0f);

        Manager.Pool.pool_dict["Monster"].Release(obj);
    }


}