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
    {   //RectTransfrom�� �ν����Ϳ��� ���� ���ö��� �ʰ�, ��ũ��Ʈ�� ����
        //�����ϴ� ��� �߰��ϴ� �ڵ�
        for (int i = 0; i < rects.Length; i++)
        {
            rects[i] = transform.GetChild(i).GetComponent<RectTransform>();
        }

        
    }

    public void Init(Vector3 pos)
    {
        target = pos;
        transform.position = Camera.main.WorldToScreenPoint(pos);
        
        //rects ���� position�� 0,0���� �̵�
        for (int i = 0;i < rects.Length;i++)
        {
            rects[i].anchoredPosition = Vector2.zero;
            //UI ������ �ʼ�
            //Vector2 anchoredPosition
            //�г��� ��Ŀ�κ��� ��ġ�� ��Ÿ��
            // --> �ν����� �󿡼� ���̴� posX, posY �� ��ġ
            
        }

        transform.parent = B_Canvas.instance.GetLayer(0);
        StartCoroutine(Move());
    }

    private float distance; //�Ÿ�
    [SerializeField] private float speed;
    //������ �������� �ڷ�ƾ���� ����
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
                // �Ÿ��� ���� ������ �����ؼ� Ż���ϴ� �ڵ�
            }

            if(CheackDistance(pos, 0.5f))
            {
                break;
            }

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        //====================== �������� ������ ȿ�� ================================
        
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

        //======================= �������� UI ���� ������ �̵��ϴ� ȿ�� ===============================================

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
            //a �� b ���� �Ÿ��� üũ�ϴ� ����

            if(distance > range)
            {
                return false;
            }
        }

        return true;
    }
}
