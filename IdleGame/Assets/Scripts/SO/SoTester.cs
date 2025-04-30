using UnityEditor.UIElements;
using UnityEngine;

public class SoTester : MonoBehaviour
{
    public Item[] item;

    private void Start()
    {
        foreach (var item in item)
        {
            Debug.Log($"아이템 이름 : {item.name} {item.description} 가격 : {item.value}");
        }
    }
}
