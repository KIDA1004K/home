using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Item_Object : MonoBehaviour
{
    public Transform ItemText;
    public Text text;

    [SerializeField] private float angle = 45.0f;
    [SerializeField] private float range = 2;
    private float gravity = 9.8f;
    
    bool ischeck = false;

    public void Init(Vector3 pos)
    {
        //���޹��� ���� �������� �� �ֺ��� ��ġ�� �� �ֵ��� ���� ����
        Vector3 item_pos = new Vector3(pos.x + (Random.insideUnitSphere.x * range), pos.y, pos.z + (Random.insideUnitSphere.z * range));

        //��ü �̵� ����
        StartCoroutine(Simulate(pos));
    }
    //������ ��� ���� ó���ϴ� �ڵ�
    private void ItemRare()
    {
        ischeck = true;
        transform.rotation = Quaternion.identity;
        ItemText.gameObject.SetActive(true);
        ItemText.parent = B_Canvas.instance.GetLayer(2);
        text.text = "������"; //������ �̸� ����
    }

    private void Update()
    {
        if(ischeck == false)
        {
            return;
        }
        ItemText.position = Camera.main.WorldToScreenPoint(transform.position);

    }

    IEnumerator Simulate(Vector3 pos)
    {
        float target_Distance = Vector3.Distance(transform.position, pos);
        float radian = angle * Mathf.Deg2Rad;
        float velocity = Mathf.Sqrt(target_Distance * gravity / Mathf.Sin(2 * radian));

        float vx = velocity * Mathf.Cos(radian);
        float vy = velocity * Mathf.Sin(radian);

        float duration = target_Distance / vx;

         transform.rotation = Quaternion.LookRotation(pos - transform.position);
        //LookAt ó�� ȸ�� ���� �ٶ󺸰� ����� �ڵ�

        float simulate_time = 0.0f;

        while (simulate_time < duration)
        {
            simulate_time += Time.deltaTime;
            transform.Translate(0, (vy - (gravity * simulate_time)), vx * Time.deltaTime);
            yield return null;

        }

        //������ �̵� �ùķ��̼��� ������ ��� üũ �� ȭ�鿡 ������ �̸� ����
        ItemRare();

    }

    //IEnumerator Simulate(Vector3 pos)
    //{
    //    // Ÿ���� �Ÿ�
    //    var targetDistance = Vector3.Distance(transform.position, pos);
    //    // ��� ���� ����
    //    var velocity = targetDistance / (Mathf.Sin(2 * angle * Mathf.Deg2Rad) / gravity);

    //    //1. ���� ����� ���� �Ѵ�.
    //    //2. Ÿ���� �Ÿ��� �߷� ���� ���� ����� ���� õõ�� �����̰Բ� �Ѵ�.

    //    //Mathf.Sin : �ﰢ�Լ� �߿��� Sin ���� ��ȯ�ϴ� ���

    //    //�ﰢ�� �������� ���� ���θ� w,h �� Sin�� h / a �� ��ȯ
    //    //����Ƽ���� ���� 45�� ��� ������ ���̰� 1�� �ﰢ���� �������
    //    //Mathf.Sin(45 * Mathf.Deg2Rad) => ������ ���̰� 1�̰� ������ 45���� �ﰢ���� ���̸� ����
    //    //Mathf.Cos(45 * Mathf.Deg2Rad) => ������ ���̰� 1�̰� ������ 45���� ��                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       ������ �غ��� ����
    //    //Deg2Rad�� ��(Degree) -> �������� �������ִ� �ڵ�
    //    //��� ���� : ����Ƽ���� sin, cos �Լ��� ����Ҷ� ������ ���� ���� ���

    //    /*
    //     * ���� ���Ǵ� Mathf �Լ�
    //     * 1. Mathf.Abs(��) : ����
    //     * 2. Mathf.Sin(sin) : ���� �� (y��)
    //     * 3. Mathf.Cos(cos) : �ڻ��� �� (x��)
    //     * 4. Mathf.Deg2Rad() : ���� -> ����
    //     * 5. Mathf.sqrt(��) : ������
    //     */

    //    float sx = Mathf.Sqrt(velocity) * Mathf.Cos(angle * Mathf.Deg2Rad);
    //    float sy = Mathf.Sqrt(velocity) * Mathf.Sin(angle * Mathf.Deg2Rad);

    //    // �������� �ϴ� �ð�
    //    float duration = targetDistance / sx;
    //    // �ð� ���� üũ
    //    float time = 0.0f;

    //    while (time < duration)
    //    {
    //        //�� ���� ���� ���� �������� ��ġ�� ������Ų��
    //        transform.Translate(0, 0, 0);
    //        time += Time.deltaTime;
    //        yield return null;
    //    }
    //}
}
