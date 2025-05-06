using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class Monster : Unit
{
    // 몬스터가 플레이어 쪽으로 이동하도록
    // 일단 0,0을 향해 가도록 하고 원형으로 랜덤 생성
    private bool isSpawn = false;
    private bool isDead = true;
    
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A) && isSpawn && isDead == false)
        {
            TakeDamage(1);
        }

        if (isSpawn)
        {
            if (transform.position.x > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, Time.deltaTime * speed);
            //transform.LookAt(Vector3.zero);
            if(transform.position == Vector3.zero)
            {
                SetAnimator("ISIDLE");
            }
        }

    }

    public void Init()
    {
        StartCoroutine(C_OnSpawn());
        HP = startHP;
    }

    protected override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        //맞을 때마다 데미지 텍스트 출력하기
        Manager.Pool.Pooling("HitText").Get(value =>
        {
            value.GetComponent<Hit>().Init(transform.position, damage);
        });

        //맞을 때마다 맞은 이펙트 출력하기

        if (HP <= 0)
        {
            OnDie();

            //이펙트 풀 생성하고 이 시점에 재생
            Manager.Pool.Pooling("EnemyDeadEF").Get(value =>
            {
                value.transform.position = new Vector3(transform.position.x,
                        transform.position.y, transform.position.z);
            });

                    }
    }

    protected override void OnDie()
    {
        Manager.Pool.Pooling("CoinMove").Get(value =>
        {
            value.GetComponent<CoinMove>().Init(transform.position);
        });

        isSpawn = false;

        base.OnDie();

        Manager.Pool.pool_dict["Enemy"].Release(gameObject);
    }


    private IEnumerator C_OnSpawn()
    {
        float current = 0.0f; // 값 저장용
        float percent = 0.0f; // 반복문 종료
        float start = 0.0f; // 시작 값
        float end = transform.localScale.x; //끝낼 값

        while (percent <= 1)
        {
            current += Time.deltaTime;
            percent = current;
            //start에서 end까지 percent 만큼
            float pos = Mathf.Lerp(start, end, percent);
            transform.localScale = new Vector3(pos, pos, pos);
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        isSpawn = true;
        isDead = false;
        SetAnimator("ISWALK");


    }

    private void SetAnimator(string temp)
    {
        animator.SetBool("ISIDLE", false); 
        animator.SetBool("ISWALK", false);

        animator.SetBool(temp, true);
        

    }







}
