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
    //    //������ ��ġ �� �����ͼ�
    //    for (int i = 0; i < Items.Length; i++)
    //    {
    //        //���鼭 ���� ��ġ�� �־��ְ�

    //        pos[i] = new Vector2(target.x, target.y) + Random.insideUnitCircle * Random.Range(-distance, distance);
    //    }

    //    while (true)
    //    {
    //        //�� ��ġ�� ���� �ӵ��� �����̰�
    //        for (int i = 0; i < Items.Length; i++)
    //        {
    //            Items[i].anchoredPosition = Vector2.MoveTowards(Items[i].anchoredPosition, pos[i], ItemMoveSpeed * Time.deltaTime);
    //        }
    //        //�� ��ġ�� �����ϸ� ���߱�
    //        if (CheakDistance(pos, 0.5f))
    //        {
    //            break;
    //        }
    //        yield return null;
    //    }

    //    yield return new WaitForSeconds(0.5f);

    //    //��ǥ ��ġ�� 
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
        // ������ ��ġ �� �����ͼ�
        for (int i = 0; i < Items.Length; i++)
        {
            // ���� ��ġ�� ����
            pos[i] = new Vector2(target.x, target.y) + Random.insideUnitCircle * Random.Range(-distance, distance);
        }

        // ���� ��ġ�� �̵�
        while (true)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                // anchoredPosition ����Ͽ� �̵�
                Items[i].anchoredPosition = Vector2.MoveTowards(Items[i].anchoredPosition, pos[i], ItemMoveSpeed * Time.deltaTime);
            }

            // ��ǥ ��ġ�� �����ߴ��� Ȯ��
            if (CheakDistance(pos, 0.5f))
            {
                break;
            }

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        // CoinUITranfrom ��ġ�� �̵�
        while (true)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                // anchoredPosition �������� �̵�
                Items[i].transform.position = Vector2.MoveTowards(Items[i].transform.position, CoinUITranfrom.transform.position, ItemSpeed * Time.deltaTime);
            }

            if (CheakDistanceItem(0.5f))
            {
                // ��ǥ ���� �� Ǯ�� ��ȯ
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
