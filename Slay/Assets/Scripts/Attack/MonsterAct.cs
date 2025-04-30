using Attack;
using UnityEngine;

public struct MonsterAct
{
    public MonsterAct(int value, Special special, Type type)
    {
        this.value = value;
        sp = special;
        this.type = type;
    }

    public int value;
    public Special sp;
    public Type type;
}
