using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class InGameDeck : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Card[] inGameDeck;
    private Card[] cards;
    public Vector2 cardPos;
    public Enemy enemy;
    public Player player;
    private int count;
    public GameObject cardPrefab;
    void Start()
    {
        //cards = Deck.deck;
        //foreach(Card card in cards)
        //{
        //    Debug.Log(card.name);
        //}
        count = 0;
        int i = 0;
        GameManager.CardList.Clear();
        inGameDeck = new Card[Deck.deck.Count];
        foreach (Card card in Deck.deck)
        {
            inGameDeck[i++] = card; 
        }
        //cards = inGameDeck;

        DeckSuffle();
        CardDrow(5);
    }

    


    public void CardDrow(int num)
    {
        GameManager.CardList.Clear();
        for (int i = 0;i < num;i++)
        {
            if (count < cards.Length)
            {
                GameObject card = Instantiate(cardPrefab, cardPos, Quaternion.identity);
                Card newCard = card.GetComponent<Card>();
                newCard.CopyData(cards[count]);
                GameManager.CardList.Add(card);
                cards[count] = default;
                newCard.Enemy = enemy;
                newCard.player = player;
                if (newCard.type == TypeCard.Attack)
                {
                    newCard.valueText = newCard.value + GameManager.playerStr;
                    newCard.ChangeText();
                }
                cardPos.x += 2.5f;
                Debug.Log(count);
                count++;
            }
            else
            {
                DeckSuffle();
                i--;
            }
        }
    }

    

    public void DeckSuffle()
    {

        for (int i = 0; i < inGameDeck.Length; i++)
        {
            int randomIndex = Random.Range(i, inGameDeck.Length);
            Card temp = inGameDeck[i];
            inGameDeck[i] = inGameDeck[randomIndex];
            inGameDeck[randomIndex] = temp;
        }
        cards = new Card[inGameDeck.Length];
        for (int i = 0; i < inGameDeck.Length ; i++)
        {
            cards[i] = inGameDeck[i];
        }
        count = 0;
        
    }

    //public void AddP()
    //{
    //    foreach (Card card in cards)
    //    {
    //        if (card != null && card.type == TypeCard.Attack)
    //            card.valueText += GameManager.playerStr;
    //    }
    //    foreach (Card card in inGameDeck)
    //    {
    //        if (card != null && card.type == TypeCard.Attack)
    //            card.valueText += GameManager.playerStr;

    //    }
    //}
    public void EndTurn()
    {
        // 남은카드 버린카드더미로
        foreach (GameObject card in GameManager.CardList)
        {
            //Destroy(card);
            if (card != null)
            {
                Destroy(card);
            }
        }
        GameManager.CardList.Clear();
        cardPos = new Vector2(-3.5f,-3f);

    }

    public void StartTurn()
    {
        Debug.Log("턴시작");
        // 뽑을카드 더미에서 카드 가져오기
        CardDrow(5);
    }
}
