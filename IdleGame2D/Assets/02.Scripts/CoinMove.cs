using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoinMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public RectTransform[] Items;
    public RectTransform CoinUITranfrom;
    public float ItemMoveSpeed;
    public float ItemSpeed;
    public float distance;
    Vector3 target;

    private void Awake()
    {
        CoinUITranfrom = B_Canvas.Instance.Coin.GetComponent<RectTransform>();
    }

    public void Init(Vector3 pos)
    {
        target = pos;
        GetComponent<RectTransform>().transform.position = Camera.main.WorldToScreenPoint(pos);

        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].anchoredPosition = Vector2.zero;
        }

        transform.parent = B_Canvas.Instance.transform;
        StartCoroutine(C_Move());
    }


    //IEnumerator C_Move()
    //{

    //    Vector2[] pos = new Vector2[Items.Length];
    //    //아이템 위치 다 가져와서
    //    for (int i = 0; i < Items.Length; i++)
    //    {
    //        //돌면서 랜덤 위치값 넣어주고

    //        pos[i] = new Vector2(target.x, target.y) + Random.insideUnitCircle * Random.Range(-distance, distance);
    //    }

    //    while (true)
    //    {
    //        //그 위치로 일정 속도로 움직이게
    //        for (int i = 0; i < Items.Length; i++)
    //        {
    //            Items[i].anchoredPosition = Vector2.MoveTowards(Items[i].anchoredPosition, pos[i], ItemMoveSpeed * Time.deltaTime);
    //        }
    //        //그 위치에 도착하면 멈추기
    //        if (CheakDistance(pos, 0.5f))
    //        {
    //            break;
    //        }
    //        yield return null;
    //    }

    //    yield return new WaitForSeconds(0.5f);

    //    //목표 위치로 
    //    while (true)
    //    {
    //        for (int i = 0; i < Items.Length; i++)
    //        {
    //            Items[i].anchoredPosition = Vector2.MoveTowards(Items[i].anchoredPosition, CoinUITranfrom.position, ItemSpeed * Time.deltaTime);

    //        }
    //        if (CheakDistanceItem(0.5f))
    //        {
    //            Manager.Pool.pool_dict["CoinMove"].Release(gameObject);
    //            yield return null;
    //            break;
    //        }


    //        yield return null;
    //    }


    //}
    IEnumerator C_Move()
    {
        Vector2[] pos = new Vector2[Items.Length];
        // 아이템 위치 다 가져와서
        for (int i = 0; i < Items.Length; i++)
        {
            // 랜덤 위치로 설정
            pos[i] = new Vector2(target.x, target.y) + Random.insideUnitCircle * Random.Range(-distance, distance);
        }

        // 랜덤 위치로 이동
        while (true)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                // anchoredPosition 사용하여 이동
                Items[i].anchoredPosition = Vector2.MoveTowards(Items[i].anchoredPosition, pos[i], ItemMoveSpeed * Time.deltaTime);
            }

            // 목표 위치에 도달했는지 확인
            if (CheakDistance(pos, 0.5f))
            {
                break;
            }

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        // CoinUITranfrom 위치로 이동
        while (true)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                // anchoredPosition 기준으로 이동
                Items[i].transform.position = Vector2.MoveTowards(Items[i].transform.position, CoinUITranfrom.transform.position, ItemSpeed * Time.deltaTime);
            }

            if (CheakDistanceItem(0.5f))
            {
                // 목표 도달 시 풀로 반환
                Manager.Pool.pool_dict["CoinMove"].Release(gameObject);
                break;
            }

            yield return null;
        }

        //    while (true)
        //    {
        //        for (int i = 0; i < Items.Length; i++)
        //        {
        //            Items[i].anchoredPosition = Vector2.MoveTowards(Items[i].anchoredPosition, CoinUITranfrom.position, ItemSpeed * Time.deltaTime);

        //        }
        //        if (CheakDistanceItem(0.5f))
        //        {
        //            Manager.Pool.pool_dict["CoinMove"].Release(gameObject);
        //            yield return null;
        //            break;
        //        }


        //        yield return null;
        //    }
    }

    private bool CheakDistanceItem(float range)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            float distance = Vector2.Distance(Items[i].gameObject.transform.position, CoinUITranfrom.gameObject.transform.position);
            if (distance > range)
            {
                return false;
            }
        }
        
        return true;
    }

    private bool CheakDistance(Vector2[] end, float range)
    {
        for (int i = 0; i < end.Length; i++)
        {
            float distance = Vector2.Distance(Items[i].anchoredPosition, end[i]);
            if (distance <= range)
                return true;
        }
        return false;
    }
}
