using System;
using System.Collections.Generic;
using Attack;
using NUnit.Framework;
using UnityEngine;

public class Pattern
{
    public List<MonsterAct> MonsterActs_list = new List<MonsterAct>();
    

    public void Monster1()
    {
        MonsterActs_list.Add(new MonsterAct(10,Special.Weak,Attack.Type.Attack));
        MonsterActs_list.Add(new MonsterAct(12,Special.Weak,Attack.Type.Attack));
        MonsterActs_list.Add(new MonsterAct(15,Special.None,Attack.Type.Attack));
        MonsterActs_list.Add(new MonsterAct(20,Special.None,Attack.Type.Attack));
        MonsterActs_list.Add(new MonsterAct(5,Special.None,Attack.Type.Attack));
        PatternList.pattern_list.Add(this);
    }

}
