using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //public GameObject strPanel;
    public TextMeshProUGUI strText;

     public override void GetPower(int value)
    {
        base.GetPower(value);
        strText.text = GameManager.playerStr.ToString();
    }












}
