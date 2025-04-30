using Attack;
using UnityEngine;

namespace Attack
{
    public class A1Class
    {
        public A1Class(int value, Type Type, Special special)
        {
            this.value = value;
            type = Type;
            this.special = special;
        }

        

        public Type type;
        public int value;
        public Special special;
        //public void Acting(Player player)
        //{
        //    if (type == Type.Attack)
        //    {
        //        player.TakeDamage(value);
        //    }

        //    GameManager.isClick = false;
            
        //}
    }
}

