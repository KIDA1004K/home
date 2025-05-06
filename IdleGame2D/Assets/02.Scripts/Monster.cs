using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class Monster : Unit
{
    // ���Ͱ� �÷��̾� ������ �̵��ϵ���
    // �ϴ� 0,0�� ���� ������ �ϰ� �������� ���� ����
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
        //���� ������ ������ �ؽ�Ʈ ����ϱ�
        Manager.Pool.Pooling("HitText").Get(value =>
        {
            value.GetComponent<Hit>().Init(transform.position, damage);
        });

        //���� ������ ���� ����Ʈ ����ϱ�

        if (HP <= 0)
        {
            OnDie();

            //����Ʈ Ǯ �����ϰ� �� ������ ���
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
        float current = 0.0f; // �� �����
        float percent = 0.0f; // �ݺ��� ����
        float start = 0.0f; // ���� ��
        float end = transform.localScale.x; //���� ��

        while (percent <= 1)
        {
            current += Time.deltaTime;
            percent = current;
            //start���� end���� percent ��ŭ
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
