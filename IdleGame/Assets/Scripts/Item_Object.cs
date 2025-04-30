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
        //전달받은 값을 기준으로 그 주변에 위치할 수 있도록 범위 설정
        Vector3 item_pos = new Vector3(pos.x + (Random.insideUnitSphere.x * range), pos.y, pos.z + (Random.insideUnitSphere.z * range));

        //물체 이동 시작
        StartCoroutine(Simulate(pos));
    }
    //아이템 레어도 별로 처리하는 코드
    private void ItemRare()
    {
        ischeck = true;
        transform.rotation = Quaternion.identity;
        ItemText.gameObject.SetActive(true);
        ItemText.parent = B_Canvas.instance.GetLayer(2);
        text.text = "아이템"; //아이템 이름 설정
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
        //LookAt 처럼 회전 방향 바라보게 만드는 코드

        float simulate_time = 0.0f;

        while (simulate_time < duration)
        {
            simulate_time += Time.deltaTime;
            transform.Translate(0, (vy - (gravity * simulate_time)), vx * Time.deltaTime);
            yield return null;

        }

        //아이템 이동 시뮬레이션이 끝나면 레어도 체크 후 화면에 아이템 이름 띄우기
        ItemRare();

    }

    //IEnumerator Simulate(Vector3 pos)
    //{
    //    // 타겟의 거리
    //    var targetDistance = Vector3.Distance(transform.position, pos);
    //    // 곡선에 대한 설정
    //    var velocity = targetDistance / (Mathf.Sin(2 * angle * Mathf.Deg2Rad) / gravity);

    //    //1. 각도 계산을 진행 한다.
    //    //2. 타겟의 거리와 중력 값을 통해 계산한 값이 천천히 움직이게끔 한다.

    //    //Mathf.Sin : 삼각함수 중에서 Sin 값을 반환하는 기능

    //    //삼각형 기준으로 가로 세로를 w,h 면 Sin은 h / a 를 반환
    //    //유니티에서 각이 45일 경우 빗변의 길이가 1인 삼각형이 만들어짐
    //    //Mathf.Sin(45 * Mathf.Deg2Rad) => 빗변의 길이가 1이고 각도가 45도인 삼각형의 높이를 리턴
    //    //Mathf.Cos(45 * Mathf.Deg2Rad) => 빗변의 길이가 1이고 각도가 45도인 삼                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       각형의 밑변을 리턴
    //    //Deg2Rad는 도(Degree) -> 라디안으로 변경해주는 코드
    //    //사용 이유 : 유니티에서 sin, cos 함수를 계산할때 각도를 라디안 으로 사용

    //    /*
    //     * 자주 사용되는 Mathf 함수
    //     * 1. Mathf.Abs(값) : 절댓값
    //     * 2. Mathf.Sin(sin) : 사인 값 (y축)
    //     * 3. Mathf.Cos(cos) : 코사인 값 (x축)
    //     * 4. Mathf.Deg2Rad() : 각도 -> 라디안
    //     * 5. Mathf.sqrt(값) : 제곱근
    //     */

    //    float sx = Mathf.Sqrt(velocity) * Mathf.Cos(angle * Mathf.Deg2Rad);
    //    float sy = Mathf.Sqrt(velocity) * Mathf.Sin(angle * Mathf.Deg2Rad);

    //    // 움직여야 하는 시간
    //    float duration = targetDistance / sx;
    //    // 시간 누적 체크
    //    float time = 0.0f;

    //    while (time < duration)
    //    {
    //        //이 로직 진행 동안 아이템의 위치를 이전시킨다
    //        transform.Translate(0, 0, 0);
    //        time += Time.deltaTime;
    //        yield return null;
    //    }
    //}
}
