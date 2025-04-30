using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class Monster : MonoBehaviour
{
    // ���Ͱ� �÷��̾� ������ �̵��ϵ���
    // �ϴ� 0,0�� ���� ������ �ϰ� �������� ���� ����
    public float speed;
    public float rotationSpeed = 2;
    private void Start()
    {
    }

    private void Update()
    {
        //Vector3 relativeTarget = (Vector3.zero - transform.position).normalized;
        ////Vector3.right if you have a sprite rotated in the right direction
        //Quaternion toQuaternion = Quaternion.FromToRotation(Vector3.right, relativeTarget);
        //transform.rotation = Quaternion.Slerp(transform.rotation, toQuaternion, rotationSpeed * Time.deltaTime);


        //Vector3 dir = Front.transform.position - transform.position;
        //float angle;
        //if (Front.position.x < 0)
        //{
        //    gameObject.GetComponent<SpriteRenderer>().flipX = false;
        //}

        //angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
    }

    
    


}
