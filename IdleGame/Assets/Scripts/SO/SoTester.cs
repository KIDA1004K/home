using UnityEditor.UIElements;
using UnityEngine;

public class SoTester : MonoBehaviour
{
    public Item[] item;

    private void Start()
    {
        foreach (var item in item)
        {
            Debug.Log($"������ �̸� : {item.name} {item.description} ���� : {item.value}");
        }
    }
}
