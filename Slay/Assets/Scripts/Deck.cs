using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Deck
{

    // ���� ���� �� ���� ���� �����߰� �������� ��� �� ���� ��û�� ���� �� ���� �������� �ؼ� �ΰ��� ������ ������ 
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
