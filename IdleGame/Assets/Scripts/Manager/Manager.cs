//using System;
//using UnityEngine;

//// ������Ʈ ���� �Ŵ����� �����ϴ� �Ŵ���(Brain) cpu 

//public class Manager : MonoBehaviour
//{
//    #region Singleton
//    // �̱��� -> ���Ϲ���
//    //1. �ڱ� �ڽſ� ���� ���� �ν��Ͻ��� �ʵ� �Ǵ� ������Ƽ�� ����
//    //�� �ʵ�� �⺻ ���� null�̺��.
//    public static Manager Instance = null;

//    //2. ������ ���۵Ǳ� �� �ܰ迡�� �ʱ�ȭ ����

//    private void Awake()
//    {
//        Init();    
//    }

//    // �ν��Ͻ��� ���Ϲ����ϴ� ��� ��~
//    private void Init()
//    {
//        //���� �ν��Ͻ��� null�� ���
//        if(Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//            //���� ������Ʈ�� DDOL�� �Ѱ���
//            //�� ���� �ִ� ������Ʈ�� ���� �ε��ص� �ı����� �ʰ� �״�� ����
//        }
//        else
//        {
//            Destroy(gameObject);
//            //null�� �ƴ϶��, �ı��ع��� (�Ѿ�� �ָ� �����ϰڴٴ� ��)
//        }

//    }
//    #endregion


//    //��ϵ� �Ŵ���
//    private static PoolManager PoolManager = new PoolManager();

//    //�Ŵ��� ������ ���� ������Ƽ
//    public static PoolManager Pool { get { return PoolManager; } }


//    public GameObject ResourceInstantiate(string path) => Resources.Load<GameObject>(path);
//}
using System;
using UnityEngine;

//������Ʈ ���� �Ŵ����� �����ϴ� �Ŵ���(Brain)
public class Manager : MonoBehaviour
{
    #region Singleton
    //1. �ڱ� �ڽſ� ���� ���� �ν��Ͻ��� �ʵ� �Ǵ� ������Ƽ�� �����ϴ�.
    //�� �ʵ�� �⺻ ���� null�Դϴ�.
    public static Manager Instance = null;

    //2. ������ ���۵Ǳ� �� �ܰ迡�� �ʱ�ȭ�� ����˴ϴ�.
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        //���� �ν��Ͻ��� null�� ���
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //���ӿ�����Ʈ�� DDOL�� �Ѱ��ݴϴ�.
            //�� ���� �ִ� ������Ʈ�� ���� �ε��ص� �ı����� �ʰ�
            //�״�� ���޵˴ϴ�.
        }
        else
        {
            Destroy(gameObject);
            //null�� �ƴ϶��, �ı��ع����ϴ�.
        }
    }
    #endregion


    //��ϵ� �Ŵ���
    private static PoolManager PoolManager = new PoolManager();

    //�Ŵ��� ������ ���� ������Ƽ
    public static PoolManager Pool { get { return PoolManager; } }




    public GameObject ResourceInstantiate(string path) => Instantiate(Resources.Load<GameObject>(path));

}