using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public InGameDeck inGameDeck;
    public Enemy enemy;
    public Player player;
    public int MaxCost;
    public int currentCost;
    public TextMeshProUGUI costText;
    public Animator animator;
    public static int playerStr;
    // 턴 종료시 적에게 턴을 넘김
    // 적 턴 종료 받아오기
    public static bool isClick = false;
    public Dictionary<string,int> valueAdd_Dict = new Dictionary<string,int>();
    public static List<GameObject> CardList = new List<GameObject>();
    


    private void Awake()
    {
        CardDataDict.MakeCardData();
        Deck.AddDeck("Attack", 3);
        Deck.AddDeck("Defance", 3);
        Deck.AddDeck("DAttack", 3);
        Deck.AddDeck("StrUp", 3);
    }

    private void Start()
    {
        
        valueAdd_Dict.Add("Attack", 0);
        valueAdd_Dict.Add("Defance", -1);
        valueAdd_Dict.Add("DAttack", -2);
        valueAdd_Dict.Add("StrUp", -4);
        currentCost = MaxCost;
        costText.text = currentCost.ToString() + " / " + MaxCost.ToString();
        

    }
    public void OnTurnEnd()
    {
        if (isClick == false)
        {
            isClick = true;
            inGameDeck.EndTurn();
            enemy.Attack();
        }
        
    }
    public void OnTurnStart()
    {
        currentCost = MaxCost;
        costText.text = currentCost.ToString() + " / " + MaxCost.ToString();
        inGameDeck.StartTurn();
        enemy.Set();
        player.OnStartTurn();
    }

    public bool attack(Card card)
    {
        if (currentCost < card.cost)
        {
            return false;
        }
        animator.SetTrigger(card.type.ToString());
        currentCost -= card.cost;
        costText.text = currentCost.ToString() + " / " + MaxCost.ToString();
        card.Exprot();
        return true;
    }

    public static void CardsRP()
    {
        foreach (GameObject card in CardList)
        {
            if (card.GetComponent<Card>().type == TypeCard.Attack)
            {
                card.GetComponent<Card>().valueText = card.GetComponent<Card>().value;
                card.GetComponent<Card>().valueText += playerStr;
                card.GetComponent<Card>().ChangeText();
                card.GetComponent<Card>().valueText = card.GetComponent<Card>().value;
            }

           
        }
    }

}
