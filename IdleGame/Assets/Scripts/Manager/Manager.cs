//using System;
//using UnityEngine;

//// 프로젝트 내의 매니저를 관리하는 매니저(Brain) cpu 

//public class Manager : MonoBehaviour
//{
//    #region Singleton
//    // 싱글톤 -> 유일무이
//    //1. 자기 자신에 대한 전역 인스턴스를 필드 또는 프로퍼티로 가짐
//    //이 필드는 기본 값이 null이빈다.
//    public static Manager Instance = null;

//    //2. 게임이 시작되기 전 단계에서 초기화 진행

//    private void Awake()
//    {
//        Init();    
//    }

//    // 인스턴스는 유일무이하다 라는 뜻~
//    private void Init()
//    {
//        //전역 인스턴스가 null일 경우
//        if(Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//            //게임 오브젝트를 DDOL로 넘겨줌
//            //이 씬에 있는 오브젝트는 씬을 로드해도 파괴되지 않고 그대로 전달
//        }
//        else
//        {
//            Destroy(gameObject);
//            //null이 아니라면, 파괴해버림 (넘어온 애를 보존하겠다는 뜻)
//        }

//    }
//    #endregion


//    //등록된 매니저
//    private static PoolManager PoolManager = new PoolManager();

//    //매니저 접근을 위한 프로퍼티
//    public static PoolManager Pool { get { return PoolManager; } }


//    public GameObject ResourceInstantiate(string path) => Resources.Load<GameObject>(path);
//}
using System;
using UnityEngine;

//프로젝트 내의 매니저를 관리하는 매니저(Brain)
public class Manager : MonoBehaviour
{
    #region Singleton
    //1. 자기 자신에 대한 전역 인스턴스를 필드 또는 프로퍼티로 가집니다.
    //이 필드는 기본 값이 null입니다.
    public static Manager Instance = null;

    //2. 게임이 시작되기 전 단계에서 초기화가 진행됩니다.
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        //전역 인스턴스가 null일 경우
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //게임오브젝트를 DDOL로 넘겨줍니다.
            //이 씬에 있는 오브젝트는 씬을 로드해도 파괴되지 않고
            //그대로 전달됩니다.
        }
        else
        {
            Destroy(gameObject);
            //null이 아니라면, 파괴해버립니다.
        }
    }
    #endregion


    //등록된 매니저
    private static PoolManager PoolManager = new PoolManager();

    //매니저 접근을 위한 프로퍼티
    public static PoolManager Pool { get { return PoolManager; } }




    public GameObject ResourceInstantiate(string path) => Instantiate(Resources.Load<GameObject>(path));

}