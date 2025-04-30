using UnityEngine;

public class HitText : MonoBehaviour
{
    Vector3 target; //대상
                    //Camera cam;     //카메라
    //public Text message; //텍스트

    //텍스트 출력 위치 보정 값
    float up = 0.0f;


    private void Start()
    {

    }

    private void Update()
    {
        var pos = new Vector3(target.x, target.y + up, target.z);
        transform.position = Camera.main.WorldToScreenPoint(pos);
        //메인 카메라 기준으로 스크린 위치로 설정합니다.
    }

    public void Init(Vector3 pos, double value)
    {
        target = pos;
        //message.text = value.ToString();

        transform.parent = B_Canvas.instance.transform;
    }

    public void Release()
    {

    }
}
