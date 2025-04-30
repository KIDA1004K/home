using System.Collections.Generic;
using UnityEngine;

public static class CardDataDict
{

    public static int value = 5;
    public static Dictionary<string,CardData> cardDict = new Dictionary<string,CardData>();

    public static void MakeCardData()
    {
        
        cardDict.Add("Attack", new CardData(value, "타격", "뎀줌", 1, 1, 0, TypeCard.Attack, Special.None));
        cardDict.Add("Defance", new CardData(value - 1, "수비", "방어도  얻음", 1, 1, 1, TypeCard.Defence, Special.None));
        cardDict.Add("DAttack", new CardData(value - 2, "이중타격", "뎀 2번 공격", 2, 1, 2 ,TypeCard.Attack, Special.None));
        cardDict.Add("StrUp", new CardData(value - 4, "체력 단련", "힘  증가", 1, 1, 3 ,TypeCard.strPower, Special.None));
    }
    
}
