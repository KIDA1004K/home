using System;
using UnityEngine;

public class CardData
{
    public int value;
    public string name;
    public string text;
    public int count;
    public int cost;
    public int spriteNum;
    public TypeCard typeCard;
    public Special sp;
    //public CardText cardText;

    public CardData(int value, string name, string text, int count, int cost, int spriteNum, TypeCard typeCard, Special sp)
    {
        this.value = value;
        this.name = name;
        this.text = text;
        //cardText = new (text,value);
        this.typeCard = typeCard;
        this.sp = sp;
        this.cost = cost;
        this.count = count;
        this.spriteNum = spriteNum;
    }
}
