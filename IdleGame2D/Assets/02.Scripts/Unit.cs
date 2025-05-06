using UnityEngine;

public class Unit : MonoBehaviour
{
    public float speed;
    public float HP;
    public float startHP = 3;
    public Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }
    protected virtual void TakeDamage(float damage)
    {
        HP -= damage;
        
    }

    protected virtual void OnDie()
    {
        //죽음이펙트
        //코인드랍
        

        //풀링 해제
        SetAnimator("ISIDLE");
    }

    private void SetAnimator(string temp)
    {
        animator.SetBool("ISIDLE", false);
        animator.SetBool("ISWALK", false);

        animator.SetBool(temp, true);


    }

}
