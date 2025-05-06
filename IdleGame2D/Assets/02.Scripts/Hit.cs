using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Hit : MonoBehaviour
{
    private TextMeshProUGUI hitText;
    public float posY;
    private Vector3 textPos;
    private void Awake()
    {
        hitText = GetComponent<TextMeshProUGUI>();
    }

    public void Init(Vector3 pos, float dmg)
    {
        posY = 0;
        textPos = pos;
        hitText.text = dmg.ToString();
        transform.position = pos;
        transform.parent = B_Canvas.Instance.transform;
        StartCoroutine(C_Release());
    }


    private void Update()
    {
        Vector3 pos = new Vector3(textPos.x, textPos.y + posY);
        transform.position = Camera.main.WorldToScreenPoint(pos);
        posY += Time.deltaTime;
        
    }

    IEnumerator C_Release()
    {
        yield return new WaitForSeconds(1.0f);
        Release();


    }

    void Release()
    {
        Manager.Pool.pool_dict["HitText"].Release(gameObject);

    }
}
