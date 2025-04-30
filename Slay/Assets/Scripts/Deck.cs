using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Deck
{

    // 게임 밖의 덱 관리 덱에 영구추가 영구삭제 기능 덱 섞는 요청이 들어올 때 덱을 랜덤으로 해서 인게임 덱으로 보내줌 
    // 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static List<Card> deck = new List<Card>();
    public static int count = 0;



    public static void AddDeck(string name, int num)
    {
        for (int i = 0; i < num; i++)
        {
            deck.Add(new Card(name));
        }

    }
}
