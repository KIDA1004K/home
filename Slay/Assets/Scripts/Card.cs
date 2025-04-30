using System.Collections;
using TMPro;
using UnityEngine;

public enum Special
{
    None = 0,
    Weak,

}

public enum TypeCard
{
    None = 0,
    Attack,
    Defence,
    strPower
}
public class Card : MonoBehaviour
{
    // 카드 정보와 카드 기능을 담은 클래스
    public string cardName;
    //public string info;
    public int valueText;
    public int cost;
    public int value;
    public int count;
    public int spriteNum;
    public string cardtext;
    public Special special;
    public TypeCard type;
    public Sprite[] sprites;
    public GameObject image;
    public TextMeshPro text;
    public TextMeshPro cardNameText;
    public TextMeshPro cardCost;
    public Enemy Enemy;
    public Player player;
    public Card(string name)
    {
        CardDataDict.cardDict.TryGetValue(name,out CardData value);

        this.cardName = name;
        this.cost = value.cost;
        this.count = value.count;
        this.value = value.value;
        valueText = value.value;
        this.special = value.sp;
        this.spriteNum = value.spriteNum;
        this.type =  value.typeCard;
        cardtext = value.text;
    }

    private void Awake()
    {
    }

    private void Start()
    {
        text.text = $"{valueText} {cardtext}";
        cardNameText.text = cardName;
        cardCost.text = cost.ToString();
        image.GetComponent<SpriteRenderer>().sprite = sprites[spriteNum];
        Debug.Log("value = " + value);              // 숫자 그대로 나오는지?
        Debug.Log("ToString = " + value.ToString()); // string으로 변환된 값은 어떤지?


    }

    public void ChangeText()
    {
        text.text = $"{valueText} {cardtext}";
    }

    // 이 카드의 타입에 따라 방어할지 공격할지 판단 후 타겟에게 벨류만큼 타입에 따른 행동을 함
    // 코스트가 있는지 확인은 어디서할까?...
    public void Exprot()
    {
        if (type == TypeCard.Attack)
        {
            Enemy.TakeDamage(value,count,player.gameObject);
        }
        if (type == TypeCard.Defence)
        {
            player.GetShield(value);
        }
        if (type == TypeCard.strPower)
        {
            player.GetPower(value);
        }
        //int index = GameManager.CardList.FindIndex((x) => x.GetComponent<Card>() == this);
        //GameManager.CardList.RemoveAt(index);
        gameObject.SetActive(false);
    }


    public void CopyData(Card other)
    {
        this.value = other.value;
        this.count = other.count;
        this.cardName = other.cardName;
        this.cost = other.cost;
        this.special = other.special;
        this.spriteNum = other.spriteNum;
        this.type = other.type;
        this.valueText = other.valueText;
        this.cardtext = other.cardtext;

    }


}
