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

    // 공격 시 생성될 오브젝트
    Dictionary<string, GameObject> attacks = new Dictionary<string, GameObject>();
    // 공격이 적중하면 적용될 이펙트
    Dictionary<string, ParticleSystem> attacks_enter = new Dictionary<string, ParticleSystem>();

    private void Awake()
    {
        //Attack 컴포넌트를 연결한 기준으로 자식 값에 접근하는 코드 GetChild(0)
        var attacks_trans = transform.GetChild(0);
        var onattacks_trans = transform.GetChild(1);

        //attacks_trans가 가지고 있는 자식 값만큼 작업 진행
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
        //전달받은 값으로 타겟 설정, 응시
        target = t;
        transform.LookAt(target);
        // 히트처리 false
        hit = false;
        //전달받은 값으로 데미지 설정
        damage = dmg;
        attack_key = key;
        attacks[attack_key].gameObject.SetActive(true);

    }

    //거리가 근접할 경우



    private void Update()
    {
        if (hit) return; //맞은상태면 리턴해서 hit 처리가 끝나고 다음 히트 작동
        
        target_pos = target.position;
        target_pos.y = 1.0f; // 타겟 위치와 y 축 맞춰주기
        transform.position = Vector3.MoveTowards(transform.position, target_pos, move_speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, target_pos) <= 0.1f)
        {
            if (target != null)
            {
                hit = true;
                //유닛이 가진 체력을 데미지만큼 감소시킴
                Monster monster = target.GetComponent<Monster>();
                if (monster != null)
                {
                    monster.GetDamage(damage);
                }
                // 감소하면 플레이어나 몬스터가 데미지 처리하는 함수가 처리되도록 설정해야 함
                //attacks[attack_key].gameObject.SetActive(false);

                //공격 명중 이펙트 플레이
                attacks_enter[attack_key].Play();
                //플레이를 통해 파티클 실행을 진행(Play on Awake 제거)

                StartCoroutine(ReleaseObject(attacks_enter[attack_key].main.duration));
                //파티클의 duration은 현재 main.duration으로 접근가능
                //일반적으로 코루틴은 Start 영역에서 진행하나, Update여도 특정 조건일 때만 실행되는 수준에서는 사용하는 경우도 있음
            }
        }

        

    }

    //일정 시간 뒤에 오브젝트 반납

    IEnumerator ReleaseObject(float time)
    {
        //전달받은 시간만큼 대기
        yield return new WaitForSeconds(time);

        //Attack에 대한 반납
        Manager.Pool.pool_dict["Attack"].Release(gameObject);
        
    }
}
