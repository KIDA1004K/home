using UnityEngine;

public class Unit : MonoBehaviour
{
    Animator animator;
    [Header("플레이어 능력치")]
    [SerializeField] protected double HP;
    [SerializeField] protected double ATK;
    [SerializeField] protected double ATK_SPEED;

    [Header("공격 범위")]
    [SerializeField] protected float A_RANGE;
    [SerializeField] protected float T_RANGE;

    [Header("타겟 위치")]
    protected Transform target;

    protected Transform attack_transform;
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void AttackObject()
    {
        Debug.Log("이벤트 테스트");
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
            //트리거를 작동시키면 바로 실행
            return;
        }

        //기본 파라미터에 대한 초기화
        //유니티 Animator에 만들어둔 parameter의 이름을 정확하게 기재합니다.
        animator.SetBool("isIDLE", false);
        animator.SetBool("isMOVE", false);

        //인자로 전달받은 값을 true로 설정
        animator.SetBool(temp, true);


    }


    //우선타격
    protected void StrikeFirst<T>(T[] targets) where T : Component
    {
        var enemys = targets;
        Transform closet = null;
        var max = T_RANGE;

        for (int i = 0; i < enemys.Length; i++)
        {
            var target_distance = Vector3.Distance(transform.position, enemys[i].transform.position);

            // 최대보다 거리 작다면
            if(target_distance < max)
            {
                //현재의 그 몬스터의 위치가 가장 가까운 거리
                closet = enemys[i].transform;
                //최대 거리 == 타겟 거리
                max = target_distance;
            }
        }
        // 가장 가까운 값을 타겟으로 등록
        target = closet;
        if(target != null)
        {
            transform.LookAt(target.position);
        }
    }
    
}
