using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//�¾��� ��, ���� ��� �ʿ� �ؽ�Ʈ�� �ߵ���
public class HitText : MonoBehaviour
{
    Vector3 target; //���
                    //Camera cam;     //ī�޶�
    public TextMeshProUGUI message; //�ؽ�Ʈ

    //�ؽ�Ʈ ��� ��ġ ���� ��
    float up = 0.0f;


    private void Start()
    {
    }

    private void Update()
    {
        var pos = new Vector3(target.x, target.y + up, target.z);
        transform.position = Camera.main.WorldToScreenPoint(pos);
        //���� ī�޶� �������� ��ũ�� ��ġ�� �����մϴ�.
        if (up <= 0.5f)
        {
            up += Time.deltaTime;
        }
    }

    public void Init(Vector3 pos, double value)
    {
        target = pos;
        message.text = value.ToString();

        transform.parent = B_Canvas.instance.GetLayer(1);
        //���� �ð� �ڿ� �ݳ� ����

        //Release();

        StartCoroutine(C_TextRelese());
    }

    IEnumerator C_TextRelese()
    {
        yield return new WaitForSeconds(1.0f);
        Release();
    }

    private void Release()
    {
        Manager.Pool.pool_dict["Hit"].Release(gameObject);
    }


    //�߰��� ����غ� ���� ��
    //�Ϲ� �������� ũ��Ƽ�� ������ ����
}