using UnityEngine;

namespace Attack
{
    public enum Type
    {
        None,
        Attack,
        Defance,
        Power
    }

  
    public struct A1
    {
        public A1(int Value, Type Type, Special Special)
        {
            value = Value;
            type = Type;
            special = Special;
        }

        public int value;
        public Type type;
        public Special special;

    }

}
