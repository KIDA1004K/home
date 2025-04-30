using UnityEngine;

public class Unit : MonoBehaviour
{
    Animator animator;
    [Header("�÷��̾� �ɷ�ġ")]
    [SerializeField] protected double HP;
    [SerializeField] protected double ATK;
    [SerializeField] protected double ATK_SPEED;

    [Header("���� ����")]
    [SerializeField] protected float A_RANGE;
    [SerializeField] protected float T_RANGE;

    [Header("Ÿ�� ��ġ")]
    protected Transform target;

    protected Transform attack_transform;
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void AttackObject()
    {
        Debug.Log("�̺�Ʈ �׽�Ʈ");
        Manager.Pool.pooling("Attack").get((value) =>
        {
            //value.transform.position = attack_transform.position;

            value.GetComponent<Attack>().Init(target, 1, "ATK01");
        }
        );
    }
    protected void SetAnimator(string temp)
    {
        if(temp == "isATTACK")
        {
            animator.SetTrigger("isATTACK");
            //Ʈ���Ÿ� �۵���Ű�� �ٷ� ����
            return;
        }

        //�⺻ �Ķ���Ϳ� ���� �ʱ�ȭ
        //����Ƽ Animator�� ������ parameter�� �̸��� ��Ȯ�ϰ� �����մϴ�.
        animator.SetBool("isIDLE", false);
        animator.SetBool("isMOVE", false);

        //���ڷ� ���޹��� ���� true�� ����
        animator.SetBool(temp, true);


    }


    //�켱Ÿ��
    protected void StrikeFirst<T>(T[] targets) where T : Component
    {
        var enemys = targets;
        Transform closet = null;
        var max = T_RANGE;

        for (int i = 0; i < enemys.Length; i++)
        {
            var target_distance = Vector3.Distance(transform.position, enemys[i].transform.position);

            // �ִ뺸�� �Ÿ� �۴ٸ�
            if(target_distance < max)
            {
                //������ �� ������ ��ġ�� ���� ����� �Ÿ�
                closet = enemys[i].transform;
                //�ִ� �Ÿ� == Ÿ�� �Ÿ�
                max = target_distance;
            }
        }
        // ���� ����� ���� Ÿ������ ���
        target = closet;
        if(target != null)
        {
            transform.LookAt(target.position);
        }
    }
    
}
