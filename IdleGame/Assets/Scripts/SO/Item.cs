using UnityEngine;


//SO : ��ũ��Ʈó�� ����ϴ� ������Ʈ
//Ư¡ : ����Ƽ�� Asset ���� ������ �� �ֽ��ϴ�.
// ����Ƽ�� �����ϰ� �����ص�, ����Ƽ ���ο� �����ִ� ������
// ������ ��� x �����ͷνḸ ���
[CreateAssetMenu(fileName = "item", menuName = "ScriptableObject/Item")]
public class Item : ScriptableObject
{
    public Sprite Item_Image;
    public string description;
    public int value;
}
