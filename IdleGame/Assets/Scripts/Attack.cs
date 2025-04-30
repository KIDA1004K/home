using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float move_speed;
    private Vector3 target_pos;
    double damage;
    private string attack_key;
    private bool hit = false;

    // ���� �� ������ ������Ʈ
    Dictionary<string, GameObject> attacks = new Dictionary<string, GameObject>();
    // ������ �����ϸ� ����� ����Ʈ
    Dictionary<string, ParticleSystem> attacks_enter = new Dictionary<string, ParticleSystem>();

    private void Awake()
    {
        //Attack ������Ʈ�� ������ �������� �ڽ� ���� �����ϴ� �ڵ� GetChild(0)
        var attacks_trans = transform.GetChild(0);
        var onattacks_trans = transform.GetChild(1);

        //attacks_trans�� ������ �ִ� �ڽ� ����ŭ �۾� ����
        for (int i = 0; i < attacks_trans.childCount; i++)
        {
            attacks.Add(attacks_trans.GetChild(i).name, attacks_trans.GetChild(i).gameObject);
        }

        for (int i = 0;i < onattacks_trans.childCount; i++)
        {
            attacks_enter.Add(onattacks_trans.GetChild(i).name, onattacks_trans.GetChild(i).GetComponent<ParticleSystem>());
        }
    }

    public void Init(Transform t, double dmg,string key)
    {
        //���޹��� ������ Ÿ�� ����, ����
        target = t;
        transform.LookAt(target);
        // ��Ʈó�� false
        hit = false;
        //���޹��� ������ ������ ����
        damage = dmg;
        attack_key = key;
        attacks[attack_key].gameObject.SetActive(true);

    }

    //�Ÿ��� ������ ���



    private void Update()
    {
        if (hit) return; //�������¸� �����ؼ� hit ó���� ������ ���� ��Ʈ �۵�
        
        target_pos = target.position;
        target_pos.y = 1.0f; // Ÿ�� ��ġ�� y �� �����ֱ�
        transform.position = Vector3.MoveTowards(transform.position, target_pos, move_speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, target_pos) <= 0.1f)
        {
            if (target != null)
            {
                hit = true;
                //������ ���� ü���� ��������ŭ ���ҽ�Ŵ
                Monster monster = target.GetComponent<Monster>();
                if (monster != null)
                {
                    monster.GetDamage(damage);
                }
                // �����ϸ� �÷��̾ ���Ͱ� ������ ó���ϴ� �Լ��� ó���ǵ��� �����ؾ� ��
                //attacks[attack_key].gameObject.SetActive(false);

                //���� ���� ����Ʈ �÷���
                attacks_enter[attack_key].Play();
                //�÷��̸� ���� ��ƼŬ ������ ����(Play on Awake ����)

                StartCoroutine(ReleaseObject(attacks_enter[attack_key].main.duration));
                //��ƼŬ�� duration�� ���� main.duration���� ���ٰ���
                //�Ϲ������� �ڷ�ƾ�� Start �������� �����ϳ�, Update���� Ư�� ������ ���� ����Ǵ� ���ؿ����� ����ϴ� ��쵵 ����
            }
        }

        

    }

    //���� �ð� �ڿ� ������Ʈ �ݳ�

    IEnumerator ReleaseObject(float time)
    {
        //���޹��� �ð���ŭ ���
        yield return new WaitForSeconds(time);

        //Attack�� ���� �ݳ�
        Manager.Pool.pool_dict["Attack"].Release(gameObject);
        
    }
}
