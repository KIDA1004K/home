using UnityEngine;

public class Unit : MonoBehaviour
{
    public int HP;
    

    public void GetDamage(int dmg)
    {
        HP -= dmg;
        if (HP <= 0)
        {
            Debug.Log("»ç¸Á");
        }
    }
}
