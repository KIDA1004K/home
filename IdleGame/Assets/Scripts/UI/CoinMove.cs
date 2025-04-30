using System.Collections;
using JetBrains.Annotations;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    Vector3 target;

    RectTransform[] rects = new RectTransform[5];

    [SerializeField] private float item_move_speed;

    
    private void Awake()
    {   //RectTransfrom을 인스펙터에서 직접 연겷라지 않고, 스크립트를 통해
        //연결하는 경우 추가하는 코드
        for (int i = 0; i < rects.Length; i++)
        {
            rects[i] = transform.GetChild(i).GetComponent<RectTransform>();
        }

        
    }

    public void Init(Vector3 pos)
    {
        target = pos;
        transform.position = Camera.main.WorldToScreenPoint(pos);
        
        //rects 들의 position을 0,0으로 이동
        for (int i = 0;i < rects.Length;i++)
        {
            rects[i].anchoredPosition = Vector2.zero;
            //UI 개발의 필수
            //Vector2 anchoredPosition
            //패널의 엥커로부터 위치를 나타냄
            // --> 인스펙터 상에서 보이는 posX, posY 의 위치
            
        }

        transform.parent = B_Canvas.instance.GetLayer(0);
        StartCoroutine(Move());
    }

    private float distance; //거리
    [SerializeField] private float speed;
    //코인의 움직임을 코루틴으로 구현
    IEnumerator Move()
    {
        var pos = new Vector2[rects.Length];

        for (int i = 0; i < rects.Length ;  i++)
        {
            pos[i] = new Vector2(target.x, target.y) + Random.insideUnitCircle * Random.Range(-distance,distance);
        }

        while (true)
        {
            for(int i = 0; i <rects.Length; i++)
            {
                var rect = rects[i];
                rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, pos[i],speed *  Time.deltaTime);
                // 거리에 대한 로직을 설계해서 탈출하는 코드
            }

            if(CheackDistance(pos, 0.5f))
            {
                break;
            }

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        //====================== 아이템이 퍼지는 효과 ================================
        
        while (true)
        {
            for (int i = 0; i < rects.Length; i++)
            {
                var rect = rects[i];

                rect.position = Vector2.MoveTowards(rect.position, B_Canvas.instance.Coin.position,speed * item_move_speed * Time.deltaTime);
            }
            if (CheckDistanceCoinUI(0.5f))
            {
                Manager.Pool.pool_dict["CoinMove"].Release(gameObject);
                break;
            }

            yield return null;
        }

        //======================= 아이템이 UI 코인 쪽으로 이동하는 효과 ===============================================

    }

    private bool CheckDistanceCoinUI(float range)
    {
        for (int i = 0; i < rects.Length; i++)
        {
            var distance = Vector2.Distance(rects[i].anchoredPosition, B_Canvas.instance.Coin.transform.position);

            if(distance > range)
            {
                return false;
            }
        }
        return true;
    }

    private bool CheackDistance(Vector2[] end, float range)
    {
        for (int i = 0; i < rects.Length; i++)
        {
            var distance = Vector2.Distance(rects[i].anchoredPosition, end[i]);
            //a 와 b 사이 거리를 체크하는 문법

            if(distance > range)
            {
                return false;
            }
        }

        return true;
    }
}
